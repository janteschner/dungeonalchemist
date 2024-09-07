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

    public Attack attack
    {
        get => _attack;
    }

    public void SetCard( Attack newAttack)
    {
        title.text = newAttack.attackName;
        description.text = newAttack.attackDescription;
        _attack = newAttack;
    }
}
