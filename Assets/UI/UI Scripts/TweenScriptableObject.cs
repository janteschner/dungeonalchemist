using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObject/UI_Elements", order = 1)]
public class TweenScriptableObject: ScriptableObject
{
    [Header("DOTween")]
    [SerializeField]
    private float tweenDuration;

    [SerializeField]
    private bool tweenSnapping;

    [SerializeField]
    private Ease easeType;

    [SerializeField]
    private float tweenDelaySeconds;

    [SerializeField]
    private int loopAmount;

    [SerializeField]
    private LoopType loopType;

    public float TweenDelaySeconds
    {
        get
        {
            return tweenDelaySeconds;
        }
        set
        {
            tweenDelaySeconds = value;
        }
    }

    public float TweenDuration
    {
        get => tweenDuration;
        set => tweenDuration = value;
    }
    public bool TweenSnapping
    {
        get => tweenSnapping;
        set => tweenSnapping = value;
    }
    public Ease EaseType
    {
        get => easeType;
        set => easeType = value;
    }

    public int LoopAmount
    {
        get => loopAmount;
        set => loopAmount = value;
    }

    public LoopType LoopType
    {
        get => loopType;
        set => loopType = value;
    }

}
