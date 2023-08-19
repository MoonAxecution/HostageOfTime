using HOT.Inventory.Item;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Container
{
    public class ContainerCompositeRoot : MonoBehaviour, IActable
    {
        [Inject] private UIManager uiManager;
        
        [SerializeField] private AssetReference containerLootingScreenAsset;
        [SerializeField] private ItemSettings[] items;
        
        private Inventory.Inventory inventory;

        private void Awake()
        {
            this.Inject();
            CreateInventory();
        }

        private void CreateInventory()
        {
            inventory = new Inventory.Inventory();
            
            foreach (ItemSettings item in items)
                inventory.AddItem(ItemSettingsToItemConverter.GetItem(item));
        }

        public async void Act()
        {
            var screen = await uiManager.OpenScreen<ContainerLootingScreen>(containerLootingScreenAsset);
            screen.Init(inventory);
        }
    }
}