using System.Collections;
using DG.Tweening;
using UnityEngine;


public class ActionButton_Tween: MonoBehaviour
{
    [SerializeField]
    private RectTransform actionButton;

    [SerializeField]
    private float tweenYValue;

    [SerializeField] TweenScriptableObject actionButton_SO;

    [SerializeField]
    private bool isSelected;

    private Tweener idleTween;


    // Start is called before the first frame update
    void Start()
    {
        ActionButton_IdleAnimation();
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public so we can access via Button
    public void ButtonSelection()
    {
        if (isSelected)
        {
            isSelected = false;
            ActionButton_DeSelectedAnimation();
        }

        else
        {
            isSelected = true;
            ActionButton_SelectedAnimation();
        }
    }
    private void ActionButton_IdleAnimation()
    {
        Sequence idleSequence = DOTween.Sequence();

        var endValueIdle = actionButton.anchoredPosition.y + tweenYValue;
        idleTween = actionButton.DOAnchorPosY(endValueIdle, actionButton_SO.TweenDuration, actionButton_SO.TweenSnapping)
            .SetEase(actionButton_SO.EaseType)
            .SetLoops(actionButton_SO.LoopAmount, actionButton_SO.LoopType);
    }

    private void ActionButton_SelectedAnimation()
    {
        //First stop IdleTween
        idleTween.Pause();

        //Play pop-up animation
        var endValueSelected = actionButton.anchoredPosition.y + 100;
        actionButton.DOAnchorPosY(endValueSelected, actionButton_SO.TweenDuration * 0.5f, actionButton_SO.TweenSnapping);

    }

    private void ActionButton_DeSelectedAnimation()
    {
        //Play pop-down animation
        var endValueSelected = actionButton.anchoredPosition.y - 100;
        actionButton.DOAnchorPosY(endValueSelected, actionButton_SO.TweenDuration * 0.5f, actionButton_SO.TweenSnapping);

        //Have a bit delay
        StartCoroutine(DelayIdleAnimation());
    }

    IEnumerator DelayIdleAnimation()
    {
        yield return new WaitForSeconds(actionButton_SO.TweenDuration * 0.5f);
        idleTween.Play();
    }
}

