using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{

    [SerializeField] public Image elementImage;
    [SerializeField] public Image frame;
    [SerializeField] public Sprite lvl1Frame;
    [SerializeField] public Sprite lvl2Frame;
    [SerializeField] public Sprite lvl3Frame;

    public TMP_Text title;
    
    public TMP_Text description;

    private Attack _attack;
    private bool _isUpgradeCard = false;

    public Attack attack
    {
        get => _attack;
    }

    public void SetCard( Attack newAttack)
    {
        title.text = newAttack.attackName;
        description.text = newAttack.attackDescription;
        _attack = newAttack;
        _isUpgradeCard = false;
        title.color = ElementFunctions.GetElementColor(newAttack.element);
        UpdateFrame(newAttack);
        SetIcon(newAttack.element);
    }
    
    public void SetUpgradeCard( Attack newAttack, Attack previousAttack)
    {
        title.text = newAttack.attackName;
        description.text = newAttack.GetAttackUpgradeDescription(previousAttack);
        _attack = newAttack;
        _isUpgradeCard = true;
        title.color = ElementFunctions.GetElementColor(newAttack.element);
        UpdateFrame(newAttack);
        SetIcon(newAttack.element);
    }
    public void SetHealingCard( Attack healingAttack)
    {
        title.text = healingAttack.attackName;
        description.text = healingAttack.attackDescription;
        _attack = healingAttack;
        title.color = ElementFunctions.GetElementColor(healingAttack.element);
        UpdateFrame(healingAttack);
        SetIcon(healingAttack.element);
    }

    private void SetIcon(Element element)
    {
        elementImage.sprite = UIMappings.Instance.getIconForElement(element);
        frame.color = ElementFunctions.GetElementColor(element);
    }

    private void UpdateFrame(Attack newAttack)
    {
        var level = LevelUpScript.Instance.GetLevelOfAttack(newAttack);
        if(level == 0)
        {
            frame.sprite = lvl1Frame;
        }
        if(level == 1)
        {
            frame.sprite = lvl2Frame;
        }
        else if(level == 2)
        {
            frame.sprite = lvl3Frame;
        }
    }
}
