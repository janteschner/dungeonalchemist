using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMappings : MonoBehaviour
{
    public static UIMappings Instance { get; private set; }

    [SerializeField] private Sprite fireIcon;
    [SerializeField] private Sprite iceIcon;
    [SerializeField] private Sprite voltIcon;
    [SerializeField] private Sprite slashIcon;
    [SerializeField] private Sprite stabIcon;
    [SerializeField] private Sprite bashIcon;
    [SerializeField] private Sprite healingIcon;

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

    public Sprite getIconForElement(Element element)
    {
        switch (element)
        {
            case Element.FIRE: return fireIcon;
            case Element.ICE: return iceIcon;
            case Element.VOLT: return voltIcon;
            case Element.SLASH: return slashIcon;
            case Element.STAB: return stabIcon;
            case Element.BASH: return bashIcon;
            case Element.HEALING: return healingIcon;
            default: return fireIcon;
        }
    }
}
