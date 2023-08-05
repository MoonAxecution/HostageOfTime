namespace HOT.Inventory.Item
{
    public class Item
    {
        public ItemType Type { get; }
        public int Id { get; }
        public string Name { get; }

        public Item(ItemType type, int id, string name)
        {
            Type = type;
            Id = id;
            Name = name;
        }
    }
}