using System;
using System.Collections.Generic;
using HOT.Inventory.Item;
using Random = UnityEngine.Random;

namespace HOT.Equipment
{
    public class Equipment
    {
        private readonly Dictionary<EquipmentType, EquipmentCell> cells;

        public bool IsWeaponSet => GetCell(EquipmentType.Weapon).Item != null;

        public event Action WeaponEquiped;
        
        public Equipment()
        {
            cells = new Dictionary<EquipmentType, EquipmentCell>();
            
            foreach (var type in (EquipmentType[])Enum.GetValues(typeof(EquipmentType)))
                cells.Add(type, new EquipmentCell());
        }

        public void Equip(HOT.Inventory.Item.Equipment equipment)
        {
            GetCell(equipment.EquipmentType).SetItem(equipment);
            
            if (equipment.EquipmentType == EquipmentType.Weapon)
                WeaponEquiped.Fire();
        }

        public EquipmentCell GetCell(EquipmentType type) => cells[type];

        public int GetHealthModifier()
        {
            Item helmet = GetCell(EquipmentType.Helment).Item;
            return (helmet as Armor)?.AdditionalHealth ?? 0;
        }
        
        public int GetDamage()
        {
            return IsWeaponSet ? GetWeaponDamage() : GetUnarmedDamage();
        }
        
        private int GetWeaponDamage()
        {
            Weapon weapon = GetCell(EquipmentType.Weapon).Item as Weapon;
            return Random.Range(weapon.MinDamage, weapon.MaxDamage + 1);
        }

        private int GetUnarmedDamage() => Random.Range(3, 6);
    }
}