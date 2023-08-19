using HOT.Inventory;
using HOT.Inventory.UI;
using UnityEngine;
using UnityEngine.UI;
using Screen = HOT.UI.Screen;

namespace HOT.Container
{
    public class ContainerLootingScreen : Screen
    {
        [Inject] private Profile.Profile profile;

        [SerializeField] private Button closeButton;
        [SerializeField] private InventoryView profileInventoryView;
        [SerializeField] private InventoryView containerInventoryView;

        private Inventory.Inventory containerInventory;
        
        protected override void OnAwaken()
        {
            closeButton.onClick.AddListener(CloseScreen);
        }

        public void Init(Inventory.Inventory containerInventory)
        {
            this.containerInventory = containerInventory;
            
            profileInventoryView.Init(profile.Cells);
            
            containerInventoryView.Init(containerInventory.Cells);
            containerInventoryView.ItemSelected += OnContainerItemSelected;
        }

        private void OnContainerItemSelected(InventoryCell cell)
        {
            if (!profile.AddItem(cell.Item)) return;
            
            containerInventory.UseItem(cell);
        }
    }
}