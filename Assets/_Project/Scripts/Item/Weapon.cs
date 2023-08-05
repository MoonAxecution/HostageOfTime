namespace HOT.Inventory.Item
{
    public class Weapon : Item
    {
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public Weapon(ItemType type, int id, string name, int minDamage, int maxDamage) : base(type, id, name)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}