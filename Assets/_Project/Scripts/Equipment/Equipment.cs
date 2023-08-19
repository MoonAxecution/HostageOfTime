using System;
using System.Collections.Generic;
using HOT.Core.Reactive;
using HOT.Inventory.Item;
using HOT.Skills;

namespace HOT.Equipment
{
    public class Equipment
    {
        private readonly Dictionary<EquipmentType, EquipmentCell> cells;
        private readonly EquipedWeaponManager equipedWeaponManager;

        public IReactiveProperty<bool> IsWeaponSet { get; }

        public Weapon Weapon => equipedWeaponManager.EquipedWeapon;
        public Skill[] WeaponSkills => equipedWeaponManager.Skills;
        public bool IsHelmetSet => GetCell(EquipmentType.Helmet).Item != null;

        public Equipment()
        {
            IsWeaponSet = new ReactiveProperty<bool>();

            cells = new Dictionary<EquipmentType, EquipmentCell>();
            CreateCells();
            
            equipedWeaponManager = new EquipedWeaponManager(GetCell(EquipmentType.Weapon));
        }

        private void CreateCells()
        {
            foreach (var type in (EquipmentType[])Enum.GetValues(typeof(EquipmentType)))
                cells.Add(type, new EquipmentCell());

            GetCell(EquipmentType.Weapon).Equiped += () =>
            {
                IsWeaponSet.Value = true;
            };
            
            GetCell(EquipmentType.Weapon).TookOff += () =>
            {
                IsWeaponSet.Value = false;
            };
        }

        public bool Equip(HOT.Inventory.Item.Equipment equipment)
        {
            EquipmentCell equipmentCell = GetCell(equipment.EquipmentType);
            
            if (equipmentCell.Item != null) return false;
            
            equipmentCell.SetItem(equipment);
            return true;
        }

        public Item TakeOff(EquipmentCell cell)
        {
            Item itemInCell = cell.Item;
            cell.TakeOffItem();

            return itemInCell;
        }

        public EquipmentCell GetCell(EquipmentType type) => cells[type];

        public int GetHealthModifier()
        {
            Item helmet = GetCell(EquipmentType.Helmet).Item;
            return (helmet as Armor)?.AdditionalHealth ?? 0;
        }
        
        public int GetDamage()
        {
            return equipedWeaponManager.GetDamage();
        }
    }
}