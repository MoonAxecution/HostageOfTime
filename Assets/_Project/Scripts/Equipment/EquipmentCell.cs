using System;
using HOT.Inventory.Item;

namespace HOT.Equipment
{
    public class EquipmentCell
    {
        public Item Item { get; private set; }
        
        public int ItemId => Item.Id;
        public bool IsFilled => Item != null;

        public event Action Equiped;
        
        public void SetItem(Item item)
        {
            Item = item;
            Equiped.Fire();
        }
    }
}