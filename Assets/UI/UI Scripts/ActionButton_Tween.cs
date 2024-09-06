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


    // Start is called before the first frame update
    void Start()
    {
        ActionButton_IdleAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //public so we can access via Button
    public void ActionButton_IdleAnimation()
    {
        var endValue = actionButton.anchoredPosition.y + tweenYValue;
        actionButton.DOAnchorPosY(endValue, actionButton_SO.TweenDuration, actionButton_SO.TweenSnapping)
            .SetEase(actionButton_SO.EaseType)
            .SetLoops(actionButton_SO.LoopAmount, actionButton_SO.LoopType);
    }
}
