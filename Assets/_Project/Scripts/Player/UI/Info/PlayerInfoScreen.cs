using HOT.Equipment;
using HOT.Inventory;
using HOT.Inventory.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI
{
    public class PlayerInfoScreen : Screen
    {
        [Inject] private Profile.Profile profile;
        
        [SerializeField] private TimeView timeView;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private EquipmentView equipmentView;
        [SerializeField] private Button closeScreenButton;

        private InventoryItemApplyer inventoryItemApplyer;
        
        protected override void OnAwaken()
        {
            closeScreenButton.onClick.AddListener(CloseScreen);
         
            inventoryItemApplyer = new InventoryItemApplyer();
            
            timeView.Init(profile.Time);
            
            inventoryView.Init(profile.Cells);
            inventoryView.ItemSelected += OnItemSelected;

            equipmentView.Init(profile.Equipment);
            equipmentView.EquipmentSelected += OnEquipmentSelected;
        }

        private void OnItemSelected(InventoryCell cell)
        {
            inventoryItemApplyer.ApplyItem(cell.Item.Type, cell);
        }

        private void OnEquipmentSelected(EquipmentCell cell)
        {
            profile.AddItem(profile.TakeOffEquipment(cell));
        }
    }
}