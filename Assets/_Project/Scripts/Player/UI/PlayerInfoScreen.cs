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

            equipmentView.Init(profile.Equipment);
        }
    }
}