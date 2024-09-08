using UnityEngine;

namespace Combat
{
    public class StatusEffectFunctions
    {
        
        public static string GetStatusName(StatusEffect statusEffect)
        {
            switch (statusEffect)
            {
                case StatusEffect.BURN:
                    return "Burn";
                case StatusEffect.FREEZE:
                    return "Freeze";
                case StatusEffect.PARALYSIS:
                    return "Paralysis";
                default:
                    return "None";
            }
        }
        public static string GetStatusHexString(StatusEffect effect)
            {
                switch (effect)
                {
                    case StatusEffect.BURN:
                        return "#d07c3c";
                    case StatusEffect.FREEZE:
                        return "#5587b5";
                    case StatusEffect.PARALYSIS:
                        return "#ffdb32";
                    default:
                        return "white";
                }
            }
            
            public static Color32 GetStatusColor(StatusEffect effect)
            {
                switch (effect)
                {
                    case StatusEffect.BURN:
                        return new Color32(208, 124, 60, 255);
                    case StatusEffect.FREEZE:
                        return new Color32(85, 125, 181, 255);
                    case StatusEffect.PARALYSIS:
                        return new Color32(255, 219, 50, 255);
                    default:
                        return new Color32(100, 200, 50, 255);
                }
            }
    }
}