using System.Linq;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(menuName = "Enemy Type")]
    public class EnemyType : ScriptableObject
    {
        [SerializeField] public int maxHealth;
        [SerializeField] public string enemyName;
        [SerializeField] public string descriptionBasic;
        [SerializeField] public string descriptionDetailed;
        [SerializeField] public Element[] immunities;
        [SerializeField] public Element[] resistances;
        [SerializeField] public Element[] weaknesses;
        [SerializeField] public Attack[] availableAttacks;
        [SerializeField] public Sprite healthbarSprite;
        [SerializeField] public GameObject prefab;
        [SerializeField] public bool bCanShoot = false;
        [SerializeField] public string id;


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