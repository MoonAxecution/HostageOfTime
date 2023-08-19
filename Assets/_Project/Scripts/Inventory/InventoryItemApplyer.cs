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
                {ItemType.Equipment, ApplyEquipment}
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
            profile.UseItem(inventoryCell);
        }

        private void ApplyEquipment(InventoryCell inventoryCell)
        {
            if (!profile.EquipEquipment(inventoryCell.Item as HOT.Inventory.Item.Equipment)) return;
            
            profile.UseItem(inventoryCell);
        }
    }
}