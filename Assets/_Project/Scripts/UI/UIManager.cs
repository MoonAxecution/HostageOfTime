using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HOT.Addressable;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.UI
{
    public class UIManager : MonoBehaviour
    {
        [Inject] private ScenesLoader scenesLoader;
        
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform loadingScreenParent;
        [SerializeField] private AssetReference loadingScreenAsset;
        [SerializeField] private Transform screenParent;

        private readonly Dictionary<string, List<Screen>> openedScreens = new Dictionary<string, List<Screen>>();

        private GameObject loadingScreen;

        public Canvas Canvas => canvas;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public async Task OpenLoadingScreen(Action successCallback = null)
        {
            var loadedScreen = await OpenScreen(loadingScreenAsset, loadingScreenParent, false, successCallback);
            loadingScreen = loadedScreen.gameObject;
        }

        public async Task<TScreen> OpenScreen<TScreen>(AssetReference screenReference, bool hidePreviousScreen = false, Action successCallback = null)
        {
            var loadedScreen = await OpenScreen(screenReference, screenParent, hidePreviousScreen, successCallback);
            StoreScreen(loadedScreen);
            
            return loadedScreen.GetComponent<TScreen>();
        }

        public async Task<Screen> OpenScreen(AssetReference screenReference, bool hidePreviousScreen = false, Action successCallback = null)
        {
            var loadedScreen = await OpenScreen(screenReference, screenParent, hidePreviousScreen, successCallback);
            StoreScreen(loadedScreen);
            
            return loadedScreen;
        }

        private void StoreScreen(Screen screen)
        {
            openedScreens.TryGetValue(scenesLoader.CurrentSceneName, out List<Screen> sceneScreen);

            if (sceneScreen != null)
            {
                sceneScreen.Add(screen);
                return;
            }

            sceneScreen = new List<Screen> {screen};
            openedScreens.Add(scenesLoader.CurrentSceneName, sceneScreen);
        }

        private async Task<Screen> OpenScreen(AssetReference screenReference, Transform parent, bool hidePreviousScreen = false, Action successCallback = null)
        {
            var loadedScreen = await LoadAsset<Screen>(screenReference, parent);

            if (hidePreviousScreen)
                openedScreens[scenesLoader.CurrentSceneName][openedScreens.Count - 1].HideScreen();

            successCallback.Fire();

            return loadedScreen;
        }
        
        public void HideAllScreens()
        {
            foreach (Screen screen in openedScreens[scenesLoader.CurrentSceneName])
                screen.HideScreen();
        }
        
        public void ShowAllScreens()
        {
            foreach (Screen screen in openedScreens[scenesLoader.CurrentSceneName])
                screen.ShowScreen();
        }

        public void CloseAllScreens()
        {
            if (!openedScreens.ContainsKey(scenesLoader.CurrentSceneName)) return;

            for (int i = openedScreens[scenesLoader.CurrentSceneName].Count - 1; i >= 0; i--)
                openedScreens[scenesLoader.CurrentSceneName][i].CloseScreen();
        }

        public void CloseScreen(Screen screen)
        {
            openedScreens[scenesLoader.CurrentSceneName].Remove(screen);
            UnloadAsset(screen.gameObject);
        }
        
        public void CloseLoadingScreen()
        {
            UnloadAsset(loadingScreen);
            loadingScreen = null;
        }

        public void LockInput()
        {
            canvasGroup.blocksRaycasts = false;
        }

        public void UnlockInput()
        {
            canvasGroup.blocksRaycasts = true;
        }

        public void UnlockScreen(Screen screen)
        {
            var group = screen.gameObject.AddComponent<CanvasGroup>();
            group.ignoreParentGroups = true;
        }

        private async Task<T> LoadAsset<T>(AssetReference asset, Transform parent) => await AddressableAssetLoader.LoadInstantiatableAsset<T>(asset, parent);

        private void UnloadAsset(GameObject asset)
        {
            AddressableAssetLoader.UnloadAsset(asset);
        }
    }
}