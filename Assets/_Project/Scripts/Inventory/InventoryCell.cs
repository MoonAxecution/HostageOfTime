using System;

namespace HOT.Inventory
{
    public class InventoryCell
    {
        private Item.Item item;
        private int count;

        public Item.Item Item => item;
        public int ItemId => item.Id;
        public bool IsFilled => item != null;

        public event Action ItemRemoved;

        public void AddItem(Item.Item item, int count = 1)
        {
            this.item = item;
            this.count = count;
        }

        public void UseItem()
        {
            count--;
            
            if (count > 0) return;
            
            RemoveItem();
        }

        private void RemoveItem()
        {
            item = null;
            ItemRemoved.Fire();
        }
    }
}