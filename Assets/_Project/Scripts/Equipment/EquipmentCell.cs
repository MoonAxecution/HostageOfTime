using System;

namespace HOT.Equipment
{
    public interface IEquipmentCellReadOnly
    {
        HOT.Inventory.Item.Equipment Item { get; }
        bool IsFilled { get; }
        
        event Action Equiped;
        event Action TookOff;
    }
    
    public class EquipmentCell : IEquipmentCellReadOnly
    {
        public HOT.Inventory.Item.Equipment Item { get; private set; }
        
        public int ItemId => Item.Id;
        public bool IsFilled => Item != null;

        public event Action Equiped;
        public event Action TookOff;
        
        public void SetItem(HOT.Inventory.Item.Equipment item)
        {
            Item = item;
            Equiped.Fire();
        }

        public void TakeOffItem()
        {
            Item = null;
            TookOff.Fire();
        }
    }
}