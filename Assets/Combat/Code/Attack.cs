using JetBrains.Annotations;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(menuName = "Attack")]
    public class Attack : ScriptableObject
    {
        [SerializeField] private int baseDamage;
        [SerializeField] private string attackName;
        [SerializeField] [CanBeNull] private string attackDescription;
        [SerializeField] private Element element;
        [SerializeField] [CanBeNull] private Attack upgradesTo;
    }
}