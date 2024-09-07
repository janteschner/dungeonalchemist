using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CardScript : MonoBehaviour
{

    [SerializeField] public Image elementImage;

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
    }
    
    public void SetUpgradeCard( Attack newAttack, Attack previousAttack)
    {
        title.text = newAttack.attackName;
        description.text = newAttack.GetAttackUpgradeDescription(previousAttack);
        _attack = newAttack;
        _isUpgradeCard = true;
        //halve the size of title
        title.fontSize = 5;
    }
    public void SetHealingCard( Attack healingAttack)
    {
        title.text = healingAttack.attackName;
        description.text = healingAttack.attackDescription;
        _attack = healingAttack;
        //halve the size of title
        title.fontSize = 10;
    }
}
