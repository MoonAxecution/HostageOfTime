using HOT.Core.Reactive;
using HOT.Equipment;
using HOT.Inventory;
using HOT.Inventory.Item;

namespace HOT.Profile
{
    public class Profile
    {
        private readonly IResourceTimeModel resourceTimeModel;
        private readonly Inventory.Inventory inventory;
        private readonly Equipment.Equipment equipment;

        public IReactiveProperty<int> Time => resourceTimeModel.Time;
        public InventoryCell[] Cells => inventory.Cells;
        public Equipment.Equipment Equipment => equipment;

        public Profile(IResourceTimeModel resourceTimeModel)
        {
            this.resourceTimeModel = resourceTimeModel;
            
            inventory = new Inventory.Inventory();
            equipment = new Equipment.Equipment();
        }

        public bool AddItem(Item item)
        {
            return inventory.AddItem(item);
        }

        public void UseItem(InventoryCell cell)
        {
            inventory.UseItem(cell);
        }
        
        public void ApplyVaccine()
        {
            resourceTimeModel.IncreaseTime(3600);
        }

        public bool EquipEquipment(HOT.Inventory.Item.Equipment item)
        {
            return equipment.Equip(item);
        }

        public Item TakeOffEquipment(EquipmentCell cell)
        {
            return equipment.TakeOff(cell);
        }
    }
}