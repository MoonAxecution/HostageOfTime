using HOT.Skills;
using UnityEngine;

namespace HOT.Inventory.Item
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "HOT/Settings/Item/Weapon")]
    public class WeaponSettings : EquipmentSettings
    {
        [SerializeField] private Skill[] skills;

        public Skill[] Skills => skills;
    }
}