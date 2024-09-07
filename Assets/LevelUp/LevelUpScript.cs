using System.Collections.Generic;
using System.Linq;
using Combat;
using UnityEngine;



public class LevelUpScript : MonoBehaviour
{
    public static LevelUpScript Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else
        { 
            Instance = this; 
        }
    }
    
    [SerializeField] private RectTransform container;
    [SerializeField] private CardScript card1;
    [SerializeField] private CardScript card2;
    [SerializeField] private CardScript card3;
    [SerializeField] private Attack[] attacksToShow;
    [SerializeField] private ElementUpgradeTree[] _upgradeTrees;
    [SerializeField] private Attack healingPotion;

    
    private List<CardScript> cardScripts = new List<CardScript>();

    // Start is called before the first frame update
    void Start()
    {
        container.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Attack[] GetStartingAttacks()
    {
        var elements = GetRandom3Elements();

        //Pick the first attack from each element's tree
        var attacks = new Attack[3];
        for (int i = 0; i < 3; i++)
        {
            var treeForElement = GetTreeForElement(elements[i]);
            attacks[i] = treeForElement.attacks[0];
        }

        return attacks;
    }

    public Attack[] GetRandomLevelUpAttacks()
    {
        var elements = GetRandom3Elements();
        //Pick the correct attack from each elements tree. Always check if the attack is already in the player's available attacks, if yes, pick the next one
        var attacks = new Attack[3];
        for (int i = 0; i < 3; i++)
        {
            var element = elements[i];
            var playerHasElement = DoesPlayerHaveAttackOfElement(element);
            var tree = GetTreeForElement(element);
            if (!playerHasElement)
            {
                Debug.Log("Player doesn't have attack of element " + element + " yet, so we're adding the first one!");
                //add the first attack from the tree
                attacks[i] = tree.attacks[0];
            }
            else
            {
                Debug.Log("Player has attack of element " + element + " already, so we're looking for the next one!");
                //search the tree for the index of the last attack the player has
                var lastAttackIndex = tree.attacks.ToList().IndexOf(PlayerManager.Instance.availableAttacks.Last(a => a.element == element));
                Debug.Log("The player's attack is number " + lastAttackIndex + " in the upgrade tree!");
                //if the attack is not the highest level, add the next attack
                if (lastAttackIndex < tree.attacks.Length - 1)
                {
                    Debug.Log("We'll pick the next attack!");
                    attacks[i] = tree.attacks[lastAttackIndex + 1];
                }
                else
                {
                    Debug.Log("That's the highest possible attack for element " + element + ", so we're adding the healing potion!");
                    //if the attack is the highest level, add the first attack as an ERROR
                    attacks[i] = healingPotion;
                }
                
            }
        }

        return attacks;
    }

    public bool DoesPlayerHaveAttackOfElement(Element element)
    {
        //search the players available attacks for an attack of the given element
        foreach (var attack in PlayerManager.Instance.availableAttacks)
        {
            if (attack.element == element)
            {
                return true;
            }
        }

        return false;
    }

    private Element[] GetRandom3Elements()
    {
        var elements = new List<Element>();
        //pick three random elements, no duplicates
        while (elements.Count < 3)
        {
            var element = (Element) Random.Range(1, 7);
            if (!elements.Contains(element))
            {
                elements.Add(element);
            }
        }

        Debug.Log("Random elements: " + elements[0] + " " + elements[1] + " " + elements[2] + "");
        return elements.ToArray();
    }

    public ElementUpgradeTree GetTreeForElement(Element element)
    {
        //return the tree for the element
        foreach (var tree in _upgradeTrees)
        {
            if (tree.element == element)
            {
                return tree;
            }
        }

        return _upgradeTrees[0];
    }

    public void ShowRandomCards()
    {
        var attacks = GetRandomLevelUpAttacks();
        Debug.Log("these are the upgrade cards: " + attacks[0].attackName + " " + attacks[1].attackName + " " + attacks[2].attackName);
        ShowCards(attacks);
    }
    
    private void ShowCards(Attack[] attacks)
    {
        int index = 0;
        foreach (var attack in attacks)
        {
            var targetCard = card1;
            if (index == 1)
            {
                targetCard = card2;
            }
            else if (index == 2)
            {
                targetCard = card3;
            }
            else if (index > 2)
            {
                return;
            }

            Debug.Log("targetCard: " + targetCard + " index: " + index + " attack: " + attack.attackName);

            if (attack.isHealingPotion)
            {
                targetCard.SetHealingCard(attack);
            }
            else
            {
                bool isCardAnUpgradeCard = DoesPlayerHaveAttackOfElement(attack.element);
                Debug.Log("Card " + attack.attackName + " is an upgrade card: " + isCardAnUpgradeCard);

                if (isCardAnUpgradeCard)
                {
                    //Search the upgrade tree for the attack that came before this one
                    var tree = GetTreeForElement(attack.element);
                    var attackIndex = tree.attacks.ToList().IndexOf(attack);
                    Debug.Log("Found attack " + attack.attackName + " at index " + attackIndex +
                              " in tree for element " + attack.element);
                    var lastAttack = tree.attacks[attackIndex - 1];

                    targetCard.SetUpgradeCard(attack, lastAttack);
                }
                else
                {
                    targetCard.SetCard(attack);
                }

            }
            cardScripts.Add(targetCard);
            index++;
        }

        container.gameObject.SetActive(true);
    }

    public int GetLevelOfAttack(Attack attack)
    {
        //go though all trees and find the index of the attack of the tree it's in
        foreach (var tree in _upgradeTrees)
        {
            var index = tree.attacks.ToList().IndexOf(attack);
            if (index != -1)
            {
                return index;
            }
        }

        return -1;
    }
    
    public void ClickOnCard(int index)
    {
        Debug.Log("Clicked on card " + index);
        OnAttackSelected(cardScripts[index].attack);
    }

    private void OnAttackSelected(Attack attack)
    {
        if (attack.isHealingPotion)
        {
            PlayerManager.Instance.Heal(attack.baseDamage);
        }
        else
        {
            if (DoesPlayerHaveAttackOfElement(attack.element))
            {
                PlayerManager.Instance.UpgradeAttack(attack);
            }
            else
            {
                PlayerManager.Instance.AddAttack(attack);
            }
            Debug.Log("Hello there!");
            // PlayerManager.Instance.AddAttack(attack);
            // container.gameObject.SetActive(false);
            // foreach (GameObject button in tempAttackButtons)
            // {
            //     Destroy(button);
            // }
            //
            // tempAttackButtons.Clear();
        }
        container.gameObject.SetActive(false);
        AfterPlayerMadeChoice();
        

    }

    private void AfterPlayerMadeChoice()
    {
        CombatManager.Instance.BeginNewCombat();
    }
}
