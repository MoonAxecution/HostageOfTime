using HOT.Equipment;

namespace HOT.Inventory.Item
{
    public class Equipment : Item
    {
        public EquipmentType EquipmentType { get; }
        
        public Equipment(ItemType type, int id, string name, EquipmentType equipmentType) : base(type, id, name)
        {
            EquipmentType = equipmentType;
        }
    }
}