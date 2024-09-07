using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComeToFront : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // [SerializeField] private Transform _transform;
    [SerializeField] TweenScriptableObject comeToFrontTween;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        var targetScale = new Vector3(1.1f, 1.1f, 1.1f);
        transform.DOScale(targetScale, comeToFrontTween.TweenDuration).SetEase(comeToFrontTween.EaseType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var targetScale = new Vector3(1f, 1f, 1f);
        transform.DOScale(targetScale, comeToFrontTween.TweenDuration).SetEase(comeToFrontTween.EaseType);
    }
}