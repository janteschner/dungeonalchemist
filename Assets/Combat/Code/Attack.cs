using JetBrains.Annotations;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(menuName = "Attack")]
    public class Attack : ScriptableObject
    {
        [SerializeField] public int baseDamage;
        [SerializeField] public string attackName;
        [SerializeField] [CanBeNull] public string attackDescription;
        [SerializeField] public Element element;
        [SerializeField] [CanBeNull] public Attack upgradesTo;
    }
}