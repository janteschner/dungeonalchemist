using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(menuName = "Enemy")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private string enemyName;
        [SerializeField] private Element[] resistances;
        [SerializeField] private Element[] weaknesses;
        [SerializeField] private Attack[] availableAttacks;
    }
}