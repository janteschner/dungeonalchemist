using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class NotebookShowHide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TweenScriptableObject notebookShowHideTween;
    [SerializeField] private RectTransform notebookTransform;
    [SerializeField] private RectTransform actionCardsTransform;
    [FormerlySerializedAs("additionalLocation")] [SerializeField] private Vector3 notebookAdditionalLocation;
    [FormerlySerializedAs("additionalRotation")] [SerializeField] private Vector3 notebookAdditionalRotation;
    [SerializeField] private Vector3 notebookTargetScale;
    [SerializeField] private Vector3 cardsAdditionalLocation;
    [SerializeField] private Vector3 cardsAdditionalRotation;
    [SerializeField] private Vector3 cardsTargetScale;


    private Vector3 notebookStartLocation;
    private Vector3 notebeookStartRotation;
    private Vector3 notebookStartingScale;
    private Vector3 cardsStartLocation;
    private Vector3 cardsStartRotation;
    private Vector3 cardsStartingScale;

    void Start()
    {
        notebookStartLocation = notebookTransform.position;
        notebeookStartRotation = notebookTransform.eulerAngles;
        notebookStartingScale = notebookTransform.localScale;
        cardsStartLocation = actionCardsTransform.position;
        cardsStartRotation = actionCardsTransform.eulerAngles;
        cardsStartingScale = actionCardsTransform.localScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        var endValueSelected = notebeookStartRotation + notebookAdditionalRotation;
        var endLocation = notebookStartLocation + notebookAdditionalLocation;
        Debug.Log("moving to " + endLocation + " and rotating to " + endValueSelected);
        notebookTransform.DOMove(endLocation, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        notebookTransform.DORotate(endValueSelected, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        notebookTransform.DOScale(notebookTargetScale, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        
        notebookTransform.SetAsLastSibling();
        
        var cardEndRotation = cardsStartRotation + cardsAdditionalRotation;
        var cardEndLocation = cardsStartLocation + cardsAdditionalLocation;
        actionCardsTransform.DOMove(cardEndLocation, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        actionCardsTransform.DORotate(cardEndRotation, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        actionCardsTransform.DOScale(cardsTargetScale, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var endValueSelected = notebeookStartRotation;
        var endLocation = notebookStartLocation;
        notebookTransform.DOMove(endLocation, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        notebookTransform.DORotate(endValueSelected, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        notebookTransform.DOScale(notebookStartingScale, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);

        notebookTransform.SetAsFirstSibling();
        
        var cardEndRotation = cardsStartRotation;
        var cardEndLocation = cardsStartLocation;
        actionCardsTransform.DOMove(cardEndLocation, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        actionCardsTransform.DORotate(cardEndRotation, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);
        actionCardsTransform.DOScale(cardsStartingScale, notebookShowHideTween.TweenDuration).SetEase(notebookShowHideTween.EaseType);

    }
}
