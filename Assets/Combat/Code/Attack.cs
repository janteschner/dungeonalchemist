﻿using JetBrains.Annotations;
using UnityEngine;

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
        
        public string attackDescription
        {
            get
            {
                //return attack description, but replace #damage with damage number
                return _attackDescription.Replace("#damage", "<b>"+ baseDamage.ToString()+"</b>")
                    .Replace("#element", "<color=" + ElementFunctions.GetElementColorHexString(element) + ">" + ElementFunctions.GetElementName(element)+"</color>");
            }
            set => _attackDescription = value;
        }

        public string GetAttackUpgradeDescription(Attack upgradeFrom)
        {
            var damageString = upgradeFrom.baseDamage + " -> " + baseDamage;
            return _attackDescription.Replace("#damage", damageString)
                .Replace("#element", element.ToString());
        }
        
    }
}