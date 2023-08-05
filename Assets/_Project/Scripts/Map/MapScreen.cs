using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HOT.UI
{
    public class MapScreen : Screen
    {
        [Header("Scenes")]
        [SerializeField] private AssetReference locationSceneReference;
        
        [Header("Buttons")]
        [SerializeField] private Button closeScreenButton;
        [SerializeField] private Button locationButton;

        protected override void OnAwaken()
        {
            closeScreenButton.onClick.AddListener(CloseScreen);
            locationButton.onClick.AddListener(MoveToLocation);
        }

        private void MoveToLocation()
        {
            LoadScene(locationSceneReference);
        }
        
        private void LoadScene(AssetReference sceneReference)
        {
            uiManager.OpenLoadingScreen(() => DependencyInjector.Resolve<ScenesLoader>().LoadScene(sceneReference));
        }
    }
}