using System.Threading.Tasks;
using HOT.Addressable;
using HOT.Auth;
using HOT.Inventory.Item;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT
{
    public class GameInitializer : MonoBehaviour
    {
        [Inject] private LoginManager loginManager;

        [Header("Settings")] 
        [SerializeField] private ItemsDatabaseSettings itemsDatabaseSettings;
        [SerializeField] private ItemSettings[] defaultItems;
        
        [Header("Assets")]
        [SerializeField] private AssetReference uiManagerAsset;
        [SerializeField] private AssetReference deadMessageScreenAsset;
        [SerializeField] private AssetReference lobbySceneAsset;

        private ResourceTimeCompositeRoot resourceTime;
        private UIManager uiManager;

        private async void Awake()
        {
            this.Inject();
            
            await CreateUIManager();
            await ShowLoadingScreen();

            CreateItemsDatabase();
            CreateTicker();
            CreateResourceTime();
            CreateProfile();

            loginManager.Auth(LoadLobby);
        }
        
        private void CreateItemsDatabase()
        {
            var itemsDatabase = new ItemsDatabase(itemsDatabaseSettings.Items);
            RegisterComponent(itemsDatabase);
        }

        private void CreateTicker()
        {
            var tickerGO = new GameObject {name = "Ticker"};
            var ticker = tickerGO.AddComponent<TickerMono>();
            RegisterComponent(ticker);
        }
        
        private void CreateResourceTime()
        {
            resourceTime = new ResourceTimeCompositeRoot(deadMessageScreenAsset);
            RegisterComponent(resourceTime);
        }

        private void CreateProfile()
        {
            var profile = new Profile.Profile(resourceTime.Model);

            foreach (ItemSettings item in defaultItems)
                profile.AddItem(ItemSettingsToItemConverter.GetItem(item));

            RegisterComponent(profile);
        }

        private async Task CreateUIManager()
        {
            uiManager = await AddressableAssetLoader.LoadInstantiatableAsset<UIManager>(uiManagerAsset);
            RegisterComponent(uiManager);
        }

        private async Task ShowLoadingScreen()
        {
            await uiManager.OpenLoadingScreen();
        }
        
        private void LoadLobby()
        {
            DependencyInjector.Resolve<ScenesLoader>().LoadSceneAsync(lobbySceneAsset);
        }


        private void RegisterComponent<T>(T component)
        {
            DependencyInjector.ReplaceComponent(component);
        }
    }
}