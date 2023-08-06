using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HOT.UI
{
    public class LobbyScreen : Screen
    {
        [Header("Screens")] 
        [SerializeField] private AssetReference playerInfoScreen;
        [SerializeField] private AssetReference mapScreen;

        [Header("Buttons")]
        [SerializeField] private Button playerInfoButton;
        [SerializeField] private Button mapButton;
        [SerializeField] private Button pvpButton;

        [Header("Scene indexes")]
        [SerializeField] private AssetReference battleSceneReference;

        protected override void OnAwaken()
        {
            playerInfoButton.onClick.AddListener(OpenPlayerInfo);
            mapButton.onClick.AddListener(OpenMap);
            pvpButton.onClick.AddListener(OpenPvP);
        }

        private void OpenPlayerInfo()
        {
            uiManager.OpenScreen(playerInfoScreen);
        }

        private void OpenMap()
        {
            uiManager.OpenScreen(mapScreen);
        }

        private void OpenPvP()
        {
            LoadScene(battleSceneReference);
        }

        private void LoadScene(AssetReference sceneReference)
        {
            uiManager.OpenLoadingScreen(() => DependencyInjector.Resolve<ScenesLoader>().LoadScene(sceneReference));
        }
    }
}