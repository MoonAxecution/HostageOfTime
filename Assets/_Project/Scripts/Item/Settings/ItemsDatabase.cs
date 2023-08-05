using System.Collections.Generic;

namespace HOT.Inventory.Item
{
    public class ItemsDatabase
    {
        private readonly Dictionary<int, ItemSettings> items;

        public ItemsDatabase(List<ItemSettings> items)
        {
            this.items = new Dictionary<int, ItemSettings>();

            foreach (ItemSettings item in items)
                this.items.Add(item.Id, item);
        }

        public ItemSettings GetItem(int itemId) => items[itemId];
    }
}