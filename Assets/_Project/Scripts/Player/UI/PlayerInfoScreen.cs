using HOT.Equipment;
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

        protected override void OnAwaken()
        {
            closeScreenButton.onClick.AddListener(CloseScreen);
            
            timeView.Init(profile.Time);
            inventoryView.Init(profile.Cells);

            equipmentView.Init(GetCell(EquipmentType.Weapon), 
                GetCell(EquipmentType.Armor),
                GetCell(EquipmentType.Boots));
        }
        
        private EquipmentCell GetCell(EquipmentType type) => profile.Equipment.GetCell(type);
    }
}