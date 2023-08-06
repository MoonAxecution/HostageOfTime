using HOT.Equipment;
using UnityEngine;

namespace HOT.Inventory.Item
{
    public class EquipmentSettings : ItemSettings
    {
        [SerializeField] private EquipmentType equipmentType;

        public EquipmentType EquipmentType => equipmentType;
    }
}