using HOT.Equipment;

namespace HOT.Inventory.Item
{
    public class Armor : Equipment
    {
        public int AdditionalHealth { get; }
        
        public Armor(ItemType type, int id, string name, EquipmentType equipmentType, int additionalHealth) 
            : base(type, id, name, equipmentType)
        {
            AdditionalHealth = additionalHealth;
        }
    }
}