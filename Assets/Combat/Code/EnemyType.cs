using System.Linq;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(menuName = "Enemy Type")]
    public class EnemyType : ScriptableObject
    {
        [SerializeField] public int maxHealth;
        [SerializeField] public string enemyName;
        [SerializeField] public Element[] immunities;
        [SerializeField] public Element[] resistances;
        [SerializeField] public Element[] weaknesses;
        [SerializeField] public Attack[] availableAttacks;

        public Attack GetRandomAttack()
        {
            if (availableAttacks.Length == 0)
            {
                return CombatManager.Instance.defaultAttack;
            }
            int r = Random.Range(0, availableAttacks.Length);
            return availableAttacks[r];
        }

        public bool IsImmune(Element element)
        {
            return immunities.Contains(element);
        }
        public bool IsResistant(Element element)
        {
            return resistances.Contains(element);
        }
        public bool IsWeak(Element element)
        {
            return weaknesses.Contains(element);
        }
    }
}