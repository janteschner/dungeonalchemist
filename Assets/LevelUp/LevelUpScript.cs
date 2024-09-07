using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform cardTemplate;
    [SerializeField] private CardScript card1;
    [SerializeField] private CardScript card2;
    [SerializeField] private CardScript card3;
    [SerializeField] private Attack[] attacksToShow;
    
    private List<CardScript> cardScripts = new List<CardScript>();

    // Start is called before the first frame update
    void Start()
    {
        ShowCards(attacksToShow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowCards(Attack[] attacks)
    {
        int index = 0;
        foreach (var attack in attacks)
        {
            var targetCard = card1;
            if (index == 1)
            {
                targetCard = card2;
            }else if (index == 2)
            {
                targetCard = card3;
            }else if (index > 2)
            {
                return;
            }
            targetCard.SetCard(attack);

            cardScripts.Add(targetCard);
            index++;

        }
        container.gameObject.SetActive(true);
        
        
    }
    
    public void ClickOnCard(int index)
    {
        Debug.Log("Clicked on card " + index);
        OnAttackSelected(cardScripts[index].attack);
    }

    private void OnAttackSelected(Attack attack)
    {
        Debug.Log("Hello there!");
        PlayerManager.Instance.AddAttack(attack);
        container.gameObject.SetActive(false);
        // foreach (GameObject button in tempAttackButtons)
        // {
        //     Destroy(button);
        // }
        //
        // tempAttackButtons.Clear();
    }
}
