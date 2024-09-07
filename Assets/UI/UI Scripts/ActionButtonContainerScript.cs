using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

public class ActionButtonContainerScript : MonoBehaviour
{
    [SerializeField] private Attack[] _DEBUG_attacks;
    [SerializeField] private GameObject _actionButtonPrefab;
    [SerializeField] private float _maxRotation = 7f;
    
    // Start is called before the first frame update
    void Start()
    {
        SetAttacks(_DEBUG_attacks);
        _actionButtonPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAttacks(Attack[] attacks)
    {
        int numberOfButtons = attacks.Length;
        float containerWidth = GetComponent<RectTransform>().rect.width;
        float spaceForEachButton = containerWidth / numberOfButtons;
        
        //extraHeight should be the difference between the container's height and the button's height
        float extraHeight = GetComponent<RectTransform>().rect.height - _actionButtonPrefab.GetComponent<RectTransform>().rect.height;
        
        float centerCardIndex = ((float)numberOfButtons - 1) / 2;
        
        Debug.Log("centerCardIndex: " + centerCardIndex);

        
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
            Debug.Log("Card #" + i + "rotation: " + rotation + " distanceFromCenter: " + distanceFromCenter);

            newButton.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, rotation);
        }
    }

    private void AppearAnimation()
    {
        
    }
}
