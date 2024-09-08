using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat
{
    [CreateAssetMenu(menuName = "Attack")]
    public class Attack : ScriptableObject
    {
        [SerializeField] public int baseDamage;
        [SerializeField] public string attackName;
        [SerializeField] [CanBeNull] private string _attackDescription;
        [SerializeField] public Element element;
        [SerializeField] public bool isHealingPotion;
        [SerializeField] public StatusEffect statusEffect = StatusEffect.NONE;
        [SerializeField] public float statusEffectChance = 0;
        
        public string attackDescription
        {
            get
            {
                //return attack description, but replace #damage with damage number
                return _attackDescription.Replace("#damage", "<b>"+ baseDamage.ToString()+"</b>")
                    .Replace("#element", "<color=" + ElementFunctions.GetElementColorHexString(element) + ">" + ElementFunctions.GetElementName(element)+"</color>")
                    .Replace("#status", "<color=" + StatusEffectFunctions.GetStatusHexString(statusEffect) + ">" + StatusEffectFunctions.GetStatusName(statusEffect)+"</color>")
                    .Replace("#chance", "<b>" + (int) Math.Round(statusEffectChance * 100, 0) +"%<b>");
            }
            set => _attackDescription = value;
        }

        public string GetAttackUpgradeDescription(Attack upgradeFrom)
        {
            var damageString = "<b>"+upgradeFrom.baseDamage + "→" + baseDamage+"</b>";
            return _attackDescription.Replace("#damage", damageString)
                .Replace("#element", "<color=" + ElementFunctions.GetElementColorHexString(element) + ">" + ElementFunctions.GetElementName(element)+"</color>")
                .Replace("#status", "<color=" + StatusEffectFunctions.GetStatusHexString(statusEffect) + ">" + StatusEffectFunctions.GetStatusName(statusEffect)+"</color>")
                .Replace("#chance", "<b>" + (int) Math.Round(upgradeFrom.statusEffectChance * 100, 0) + "→" + (int) Math.Round(statusEffectChance * 100, 0) +"%<b>");
        }
        
        public bool CalculateStatusEffect()
        {
            return Random.value < statusEffectChance;
        }
        
    }
}