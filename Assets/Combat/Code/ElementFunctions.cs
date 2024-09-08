using System.Collections;
using System.Collections.Generic;
using Combat.Player_Attacks.Combos;
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
                return "Pierce";
            case Element.BASH:
                return "Bash";
            default:
                return "Untyped";
        }
    }

    public static bool IsMagicalElement(Element element)
    {
        return element == Element.FIRE || element == Element.ICE || element == Element.VOLT;
    }

    public static Element GetComboElement(Combo combo)
    {
        switch (combo)
        {
               case Combo.NONE: return Element.SLASH;
               case     Combo.ICE_HAMMER: return Element.ICE;
               case Combo.FLAME_BLADE: return Element.FIRE;
               case     Combo.VOLT_BLADE: return Element.VOLT;
               case Combo.FIRE_ICE: return Element.FIRE;
               case     Combo.ICE_FIRE: return Element.ICE;
               case Combo.THORS_HAMMER: return Element.BASH;
               default: return Element.SLASH;
        }
    }
    
    public static string GetElementColorHexString(Element element)
    {
        switch (element)
        {
            case Element.FIRE:
                return "#d07c3c";
            case Element.ICE:
                return "#5587b5";
            case Element.VOLT:
                return "#ffdb32";
            case Element.SLASH:
                return "#346666";
            case Element.STAB:
                return "#346666";
            case Element.BASH:
                return "#346666";
            case Element.HEALING:
                return "#619652";
            default:
                return "white";
        }
    }
    
    public static Color32 GetElementColor(Element element)
    {
        switch (element)
        {
            case Element.FIRE:
                return new Color32(208, 124, 60, 255);
            case Element.ICE:
                return new Color32(85, 125, 181, 255);
            case Element.VOLT:
                return new Color32(255, 219, 50, 255);
            case Element.SLASH:
                return new Color32(52, 102, 102, 255);
            case Element.STAB:
                return new Color32(52, 102, 102, 255);
            case Element.BASH:
                return new Color32(52, 102, 102, 255);
            case Element.HEALING:
                return new Color32(97, 150, 82, 255);
            default:
                return new Color32(100, 200, 50, 255);
        }
    }
}
