using System;
using HOT.Equipment;
using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.UI
{
    public class EquipmentView : MonoBehaviour
    {
        [Inject] private ItemsDatabase itemsDatabase;
        
        [SerializeField] private EquipmentCellView weaponCellView;
        [SerializeField] private EquipmentCellView armorCellView;
        [SerializeField] private EquipmentCellView bootsCellView;

        private EquipmentCell weaponCell;
        private EquipmentCell armorCell;
        private EquipmentCell bootsCell;

        public void Init(EquipmentCell weaponCell, EquipmentCell armorCell, EquipmentCell bootsCell)
        {
            this.Inject();

            this.weaponCell = weaponCell;
            this.weaponCell.Equiped += UpdateWeaponCell;
            
            this.armorCell = armorCell;
            this.armorCell.Equiped += UpdateArmorCell;

            this.bootsCell = bootsCell;
            this.bootsCell.Equiped += UpdateBootsCell;

            UpdateWeaponCell();
            UpdateArmorCell();
            UpdateBootsCell();
        }

        private void UpdateWeaponCell()
        {
            weaponCellView.InitIcon(GetItemIcon(weaponCell));
        }
        
        private void UpdateArmorCell()
        {
            armorCellView.InitIcon(GetItemIcon(armorCell));
        }
        
        private void UpdateBootsCell()
        {
            bootsCellView.InitIcon(GetItemIcon(bootsCell));
        }

        private Sprite GetItemIcon(EquipmentCell cell) => cell.IsFilled ? GetItemSettings(cell.ItemId).Icon : null;
        
        private ItemSettings GetItemSettings(int itemId) => itemsDatabase.GetItem(itemId);

        private void OnDestroy()
        {
            this.weaponCell.Equiped -= UpdateWeaponCell;
            this.armorCell.Equiped -= UpdateArmorCell;
            this.bootsCell.Equiped -= UpdateBootsCell;
        }
    }
}