using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatButton : MonoBehaviour
{

    [SerializeField] private ActionButton_Tween _actionButtonTween;
    [SerializeField] private CardScript _card;
    public void ClickCard()
    {
        if (PlayerManager.Instance.FirstAttack == null)
        {
            _actionButtonTween.ButtonSelection();
            PlayerManager.Instance.ChoseAttackOne(_card.attack);
        }
        else
        {
            if (PlayerManager.Instance.FirstAttack == _card.attack)
            {
                //First attack deselected
                _actionButtonTween.ButtonSelection();
                PlayerManager.Instance.DeselectAttackOne();
            }
            else
            {
             //Second attack chosen   
                PlayerManager.Instance.ChoseAttackTwo(_card.attack);
                _actionButtonTween.ButtonSelection();
                ActionButtonContainerScript.Instance.SelectedSecondAttack();
            }
        }
    }

    public void RemoveButton()
    {
        //Get Button from gameObject
        var button = GetComponent<Button>();
        //Remove button from parent
        button.enabled = false;
    }
}
