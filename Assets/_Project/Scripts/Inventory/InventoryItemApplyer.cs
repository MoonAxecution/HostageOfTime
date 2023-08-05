using System;
using System.Collections.Generic;
using HOT.Data;
using HOT.Inventory.Item;

namespace HOT.Inventory
{
    public class InventoryItemApplyer
    {
        [Inject] private Profile.Profile profile;
        [Inject] private CrossSessionData crossSessionData;
        
        private readonly Dictionary<ItemType, Action<InventoryCell>> itemActions;

        public InventoryItemApplyer()
        {
            this.Inject();
            
            itemActions = new Dictionary<ItemType, Action<InventoryCell>>
            {
                {ItemType.Vaccine, ApplyVaccine},
                {ItemType.Weapon, ApplyWeapon}
            };
        }

        public void ApplyItem(ItemType itemType, InventoryCell cell)
        {
            itemActions[itemType].Fire(cell);
        }

        private void ApplyVaccine(InventoryCell inventoryCell)
        {
            crossSessionData.DieTime += 3600;
            profile.ApplyVaccine();
            inventoryCell.UseItem();
        }

        private void ApplyWeapon(InventoryCell inventoryCell)
        {
            profile.EquipWeapon(inventoryCell.Item as Weapon);
            inventoryCell.UseItem();
        }
    }
}