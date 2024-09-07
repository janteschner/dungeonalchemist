using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class NotebookShowHide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static NotebookShowHide Instance { get; private set; }
    [SerializeField] TweenScriptableObject notebookShowHideTween;
    [SerializeField] private RectTransform notebookTransform;
    [SerializeField] private RectTransform actionCardsTransform;
    [FormerlySerializedAs("additionalLocation")] [SerializeField] private Vector3 notebookAdditionalLocation;
    [FormerlySerializedAs("additionalRotation")] [SerializeField] private Vector3 notebookAdditionalRotation;
    [SerializeField] private Vector3 notebookTargetScale;
    [SerializeField] private Vector3 cardsAdditionalLocation;
    [SerializeField] private Vector3 cardsAdditionalRotation;
    [SerializeField] private Vector3 cardsTargetScale;
    [SerializeField] private float completelyHiddenYOffset = 700f;

    private Vector3 completelyHiddenLocation;


    private Vector3 notebookStartLocation;
    private Vector3 notebookStartRotation;
    private Vector3 notebookStartingScale;
    private Vector3 cardsStartLocation;
    private Vector3 cardsStartRotation;
    private Vector3 cardsStartingScale;

    private bool completelyHidden = true;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    void Start()
    {
        notebookStartLocation = notebookTransform.position;
        notebookStartRotation = notebookTransform.eulerAngles;
        notebookStartingScale = notebookTransform.localScale;
        cardsStartLocation = actionCardsTransform.position;
        cardsStartRotation = actionCardsTransform.eulerAngles;
        cardsStartingScale = actionCardsTransform.localScale;
        completelyHiddenLocation = notebookStartLocation + new Vector3(0, completelyHiddenYOffset, 0);
        transform.SetPositionAndRotation(completelyHiddenLocation, transform.rotation);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (completelyHidden) return;
        var endValueSelected = notebookStartRotation + notebookAdditionalRotation;
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
        if (completelyHidden) return;
        var endValueSelected = notebookStartRotation;
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

    public void CompletelyHideNotebook()
    {
        completelyHidden = true;
        
        notebookTransform.DOMove(completelyHiddenLocation, 0.5f).SetEase(Ease.InOutCubic);
    }
    
    public void ShowFromCompletelyHidden()
    {
        completelyHidden = false;
        
        notebookTransform.DOMove(notebookStartLocation, 0.5f).SetEase(Ease.InOutCubic);
        notebookTransform.DORotate(notebookStartRotation, 0.1f).SetEase(notebookShowHideTween.EaseType);
        notebookTransform.DOScale(notebookStartingScale, 0.1f).SetEase(notebookShowHideTween.EaseType);

    }
}
