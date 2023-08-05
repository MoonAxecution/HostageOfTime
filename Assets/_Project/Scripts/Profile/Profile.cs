using HOT.Core.Reactive;
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

        public void AddItem(Item item)
        {
            inventory.AddItem(item);
        }

        public void ApplyVaccine()
        {
            resourceTimeModel.IncreaseTime(3600);
        }

        public void EquipWeapon(Weapon weapon)
        {
            equipment.Equip(weapon);
        }
    }
}