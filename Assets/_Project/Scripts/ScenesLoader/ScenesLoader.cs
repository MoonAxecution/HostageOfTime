using System;
using System.Threading.Tasks;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace HOT
{
    public class ScenesLoader
    {
        [Inject] private UIManager uiManager;

        private const int FakeLoadingDelayMs = 1300;
        
        private LoadedScene previousScene;
        private LoadedScene currentScene;

        public event Action SceneLoaded;

        public string CurrentSceneName => currentScene.SceneName ?? string.Empty;
        public bool IsCurrentSceneAdditive => currentScene.IsAdditive;

        public ScenesLoader()
        {
            this.Inject();
        }
        
        public void LoadPreviousScene()
        {
            ShowLoadingScreen(LoadPreviousSceneInternal);
        }

        private async void LoadPreviousSceneInternal()
        {
            uiManager.CloseAllScreens();

            await Task.Delay(FakeLoadingDelayMs);
            
            Addressables.UnloadSceneAsync(currentScene.Scene).Completed += (_) =>
            {
                currentScene = previousScene;

                EnableObjectsOfCurrentScene();
                
                uiManager.ShowAllScreens();
                uiManager.CloseLoadingScreen();
            };
        }
        
        public void LoadScene(AssetReference sceneReference)
        {
            LoadSceneInternal(sceneReference, false);
        }

        public void LoadSceneWithLoadingScreen(AssetReference sceneReference, bool isAdditiveScene = false)
        {
            ShowLoadingScreen(() => LoadSceneInternal(sceneReference, isAdditiveScene));
        }

        private void ShowLoadingScreen(Action actionAfterLoading)
        {
            uiManager.OpenLoadingScreen(actionAfterLoading);
        }

        private async void LoadSceneInternal(AssetReference sceneReference, bool isAdditiveScene = false)
        {
            if (isAdditiveScene)
                uiManager.HideAllScreens();
            else
                uiManager.CloseAllScreens();

            await LoadSceneAsync(sceneReference, isAdditiveScene);
        }

        public async Task LoadSceneAsync(AssetReference sceneReference, bool isAdditiveScene = false)
        {
            await Task.Delay(FakeLoadingDelayMs);

            Addressables.LoadSceneAsync(sceneReference, isAdditiveScene ? LoadSceneMode.Additive : LoadSceneMode.Single)
                .Completed += (operationHandle) =>
            {
                previousScene = currentScene;
                currentScene = new LoadedScene(operationHandle, isAdditiveScene);
                
                SceneLoaded.Fire();
                
                if (!isAdditiveScene) return;

                DisableObjectsOfCurrentScene();
            };
        }

        private void EnableObjectsOfCurrentScene()
        {
            foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
                go.SetActive(true);
        }
        
        private void DisableObjectsOfCurrentScene()
        {
            foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
                go.SetActive(false);
        }
    }
}