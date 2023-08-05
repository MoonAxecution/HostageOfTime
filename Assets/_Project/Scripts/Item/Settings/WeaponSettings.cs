using UnityEngine;

namespace HOT.Inventory.Item
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "HOT/Settings/Item/Weapon")]
    public class WeaponSettings : ItemSettings
    {
        [SerializeField] private int minDamage;
        [SerializeField] private int maxDamage;

        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
    }
}