using HOT.Equipment;
using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.UI
{
    public class EquipmentView : MonoBehaviour
    {
        [Inject] private ItemsDatabase itemsDatabase;

        [SerializeField] private EquipmentCellView[] cellViews;

        private Equipment.Equipment equipment;
        
        public void Init(Equipment.Equipment equipment)
        {
            this.Inject();
            this.equipment = equipment;

            foreach (var cellView in cellViews)
            {
                EquipmentCell equipmentCell = equipment.GetCell(cellView.EquipmentType);
                cellView.Init(equipmentCell, GetItemIcon(equipmentCell));
                cellView.CellUpdated += OnEquipmentCellUpdated;
            }
        }

        private void OnEquipmentCellUpdated(EquipmentCellView cellView)
        {
            cellView.InitIcon(GetItemIcon(equipment.GetCell(cellView.EquipmentType)));
        }

        private Sprite GetItemIcon(EquipmentCell cell) => cell.IsFilled ? GetItemSettings(cell.ItemId).Icon : null;
        
        private ItemSettings GetItemSettings(int itemId) => itemsDatabase.GetItem(itemId);
    }
}