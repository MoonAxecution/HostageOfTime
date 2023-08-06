using System;

namespace HOT.Equipment
{
    public class EquipmentCell
    {
        public HOT.Inventory.Item.Equipment Item { get; private set; }
        
        public int ItemId => Item.Id;
        public bool IsFilled => Item != null;

        public event Action Equiped;
        
        public void SetItem(HOT.Inventory.Item.Equipment item)
        {
            Item = item;
            Equiped.Fire();
        }
    }
}