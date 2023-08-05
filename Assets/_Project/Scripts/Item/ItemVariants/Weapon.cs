using HOT.Equipment;

namespace HOT.Inventory.Item
{
    public class Weapon : Equipment
    {
        public int MinDamage { get; }
        public int MaxDamage { get; }

        public Weapon(ItemType type, int id, string name, EquipmentType equipmentType, int minDamage, int maxDamage) : base(type, id, name, equipmentType)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
        }
    }
}