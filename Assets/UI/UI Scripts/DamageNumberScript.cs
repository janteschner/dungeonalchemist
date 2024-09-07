using System.Collections;
using System.Collections.Generic;
using Combat;
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

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        StartCoroutine(Animate());
    }
    
    IEnumerator Animate()
    {
        //slowly move panel up, then delete after 1 second
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            // _panel.transform.position += new Vector3(0, 30f, 0) * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
