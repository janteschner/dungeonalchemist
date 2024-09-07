using System;
using Combat;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class CardScript : MonoBehaviour
{

    [SerializeField] public Image elementImage;

    public TMP_Text title;
    
    public TMP_Text description;

    public Attack attack;



    public void SetCard( Attack newAttack)
    {
        title.text = newAttack.attackName;
        description.text = newAttack.attackDescription;
        attack = newAttack;
    }
}
