using System;
using System.Collections.Generic;
using HOT.Inventory.Item;
using Random = UnityEngine.Random;

namespace HOT.Equipment
{
    public class Equipment
    {
        private readonly Dictionary<EquipmentType, EquipmentCell> cells;

        public IReadOnlyDictionary<EquipmentType, EquipmentCell> Cells => cells;
        public bool IsWeaponSet => GetCell(EquipmentType.Weapon).Item != null;

        public event Action WeaponEquiped;
        
        public Equipment()
        {
            cells = new Dictionary<EquipmentType, EquipmentCell>();
            
            foreach (var type in (EquipmentType[])Enum.GetValues(typeof(EquipmentType)))
                cells.Add(type, new EquipmentCell());
        }
        
        public void EquipWeapon(Item item)
        {
            GetCell(EquipmentType.Weapon).SetItem(item);
            WeaponEquiped.Fire();
        }

        public int GetDamageModifier()
        {
            Weapon weapon = GetCell(EquipmentType.Weapon).Item as Weapon;
            return Random.Range(weapon.MinDamage, weapon.MaxDamage + 1);
        }

        public EquipmentCell GetCell(EquipmentType type) => cells[type];
    }
}