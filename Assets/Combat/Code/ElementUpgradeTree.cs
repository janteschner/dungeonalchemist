using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(menuName = "ElementUpgradeTree")]
    public class ElementUpgradeTree : ScriptableObject
    {
        [SerializeField] public Element element;
        [SerializeField]  public Attack[] attacks;
    }
}
