using System.Threading.Tasks;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT
{
    public class SceneInitializer : MonoBehaviour
    {
        [SerializeField] private AssetReference defaultScreen;
        
        [Inject] private UIManager uiManager;

        private async void Awake()
        {
            this.Inject();

            await CreateDefaultScreen();
            
            uiManager.CloseLoadingScreen();
            
            OnAwaken();
        }
        
        protected virtual void OnAwaken() {}

        private async Task CreateDefaultScreen()
        {
            if (!defaultScreen.RuntimeKeyIsValid()) return;
            
            await uiManager.OpenScreen<LobbyScreen>(defaultScreen);
        }
    }
}