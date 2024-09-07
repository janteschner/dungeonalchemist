using System.Collections;
using System.Collections.Generic;
using Combat;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumberScript : MonoBehaviour
{

    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _outline;
    [SerializeField] private Image _background;
    [SerializeField] private Image _weak;
    [SerializeField] private Image _resistant;
    [SerializeField] private Image _immune;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private TweenScriptableObject _appearTween;
    
    
    // Start is called before the first frame upda

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(DamageNumberWithInfo damageInfo)
    {
        _text.text = damageInfo.damage.ToString();
        
        Debug.Log("color for element: " + ElementFunctions.GetElementColor(damageInfo.element));
        _outline.color = ElementFunctions.GetElementColor(damageInfo.element);
        
        _weak.gameObject.SetActive(damageInfo.isWeak);
        _resistant.gameObject.SetActive(damageInfo.isResistant);
        _immune.gameObject.SetActive(damageInfo.isImmune);
        StartCoroutine(DestroyAfter1Second());
        
        var normalSize = new Vector3(0.5f, 0.5f, 0.5f);
        Debug.Log("normalSize: " + normalSize);

        var targetScale = normalSize * 1.2f;
        var elasticity = 0.33f;
        var vibrato = 10;
        if (damageInfo.isWeak)
        {
            targetScale = normalSize * 1.4f;
            vibrato = 15;
            elasticity = 0.4f;
        }

        if (damageInfo.isResistant)
        {
            targetScale = normalSize * 1.1f;
            vibrato = 5;
            elasticity = 0.22f;
        }

        if (damageInfo.isImmune)
        {
            targetScale = normalSize * 1.1f;
            vibrato = 3;
            elasticity = 0.1f;
        }
        transform.DOPunchScale(targetScale, 0.4f, vibrato, elasticity);

    }

    void DisappearAnimation()
    {
        var targetScale = Vector3.zero;
        transform.DOScale(targetScale, 0.3f).SetEase(Ease.InOutCubic);
    }
    
    IEnumerator DestroyAfter1Second()
    {
        yield return new WaitForSeconds(1f);
        DisappearAnimation();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
