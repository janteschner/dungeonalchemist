using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionButtonContainerScript : MonoBehaviour
{
    public static ActionButtonContainerScript Instance { get; private set; }

    [SerializeField] private GameObject _actionButtonPrefab;
    [SerializeField] private float _maxRotation = 7f;
    [SerializeField] private float _yDeltaToDisappeared = 800f;
    [SerializeField] private TweenScriptableObject _disappearTween;
    
    private List<GameObject> _actionButtons = new List<GameObject>();

    private Vector3 normalTransform;
    private Vector3 disappearedTransform;
    
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
    
    // Start is called before the first frame update
    void Start()
    {
        normalTransform = transform.position;
        disappearedTransform = new Vector3(normalTransform.x, normalTransform.y - _yDeltaToDisappeared, normalTransform.z);
        _actionButtonPrefab.SetActive(false);
    }

    public void SetAttacks(Attack[] attacks)
    {

        int numberOfButtons = attacks.Length;
        float containerWidth = GetComponent<RectTransform>().rect.width;
        float spaceForEachButton = containerWidth / numberOfButtons;
        
        //extraHeight should be the difference between the container's height and the button's height
        float extraHeight = GetComponent<RectTransform>().rect.height - _actionButtonPrefab.GetComponent<RectTransform>().rect.height;
        
        float centerCardIndex = ((float)numberOfButtons - 1) / 2;
        

        
        for(int i = 0; i < numberOfButtons; i++)
        {
            var newButton = Instantiate(_actionButtonPrefab, transform, false);
            newButton.GetComponent<CardScript>().SetCard(attacks[i]);
            newButton.SetActive(true);
            float y = 0;
            
            var distanceFromCenter = Mathf.Abs(centerCardIndex - i);
            var relativeDistance = distanceFromCenter / numberOfButtons * 2;
            
            y = relativeDistance * extraHeight * -1;
            
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * spaceForEachButton - (containerWidth/2) + (_actionButtonPrefab.GetComponent<RectTransform>().rect.width / 2), y);
            //Rotate the card away from the center
            var invertRotation = i < centerCardIndex;
            var rotation = Mathf.Lerp(0, _maxRotation * (invertRotation ? 1 : -1), relativeDistance);

            newButton.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, rotation);
            _actionButtons.Add(newButton);
        }
    }

    public void AppearAnimation()
    {
        SetAttacks(PlayerManager.Instance.availableAttacks.ToArray());
        transform.SetPositionAndRotation(disappearedTransform, Quaternion.identity);
        transform.DOMove(normalTransform, _disappearTween.TweenDuration, _disappearTween.TweenSnapping).SetEase(_disappearTween.EaseType);
    }

    public void DisappearAnimation()
    {
        Debug.Log("Beginning Disappear Animation");
        //Set own alpha to 0
        // var group = GetComponent<CanvasGroup>();
        // group.alpha = 0;
        //Destroy all actionbuttons
        Debug.Log(_actionButtons.Count);
        foreach (var button in _actionButtons)
        {
            Destroy(button);
        }
        _actionButtons.Clear();
        
    }

    private void DisableButtons()
    {
        // //Go through all children and remove all onClick events from their buttons
        foreach (var button in _actionButtons)
        {
            button.GetComponent<CombatButton>().RemoveButton();
        }
    }

    public void SelectedSecondAttack()
    {
        StartCoroutine(BeginDisappear());
    }
    
    public IEnumerator BeginDisappear()
    {
        
        Debug.Log("BeginDisappear!");
        DisableButtons();
        yield return new WaitForSeconds(0.5f);
        transform.DOMove(disappearedTransform, _disappearTween.TweenDuration, _disappearTween.TweenSnapping).SetEase(_disappearTween.EaseType);
        yield return new WaitForSeconds(0.5f);
        DisappearAnimation();
        CombatManager.Instance.Combat();
        // Code to execute after the delay
    }
}
