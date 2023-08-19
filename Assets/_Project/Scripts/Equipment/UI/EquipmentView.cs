using System;
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

        public event Action<EquipmentCell> EquipmentSelected;
        
        public void Init(Equipment.Equipment equipment)
        {
            this.Inject();
            this.equipment = equipment;

            foreach (EquipmentCellView cellView in cellViews)
                CreateCell(cellView);
        }

        private void CreateCell(EquipmentCellView cellView)
        {
            EquipmentCell equipmentCell = equipment.GetCell(cellView.EquipmentType);
            cellView.Init(equipmentCell, GetItemIcon(equipmentCell));
            cellView.Selected += OnCellSelected;
            cellView.Updated += OnCellUpdated;
        }
        
        private void OnCellSelected(EquipmentCellView cellView)
        {
            if (cellView.Cell.Item == null) return;
            
            EquipmentSelected.Fire(cellView.Cell);
        }

        private void OnCellUpdated(EquipmentCellView cellView)
        {
            cellView.UpdateIcon(GetItemIcon(equipment.GetCell(cellView.EquipmentType)));
        }

        private Sprite GetItemIcon(EquipmentCell cell) => cell.IsFilled ? GetItemSettings(cell.ItemId).Icon : null;
        
        private ItemSettings GetItemSettings(int itemId) => itemsDatabase.GetItem(itemId);
    }
}