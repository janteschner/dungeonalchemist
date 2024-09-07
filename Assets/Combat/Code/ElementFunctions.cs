using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFunctions : MonoBehaviour
{
    public static string GetElementName(Element element)
    {
        switch (element)
        {
            case Element.FIRE:
                return "Fire";
            case Element.ICE:
                return "Ice";
            case Element.VOLT:
                return "Volt";
            case Element.SLASH:
                return "Slash";
            case Element.STAB:
                return "Stab";
            case Element.BASH:
                return "Bash";
            default:
                return "Untyped";
        }
    }
    
    public static string GetElementColor(Element element)
    {
        switch (element)
        {
            case Element.FIRE:
                return "#FF4500";
            case Element.ICE:
                return "#015493";
            case Element.VOLT:
                return "#FFD700";
            case Element.SLASH:
                return "red";
            case Element.STAB:
                return "red";
            case Element.BASH:
                return "red";
            default:
                return "red";
        }
    }
}
