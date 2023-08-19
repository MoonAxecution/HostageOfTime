using HOT.Battle;
using HOT.Creature;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HOT.UI
{
    public class LobbyScreen : Screen
    {
        [Inject] private Profile.Profile profile;
        
        [Header("Screens")]
        [SerializeField] private AssetReference mapScreen;

        [Header("Buttons")]
        [SerializeField] private Button mapButton;
        [SerializeField] private Button pvpButton;

        [Header("Scene indexes")]
        [SerializeField] private AssetReference battleSceneReference;
        
        //TODO: Переделать на генерацию снаряжения
        [Header("Settings")] 
        [SerializeField] private EnemyBattleSetup[] enemySetups;

        protected override void OnAwaken()
        {
            mapButton.onClick.AddListener(OpenMap);
            pvpButton.onClick.AddListener(OpenPvP);
        }

        private void OpenMap()
        {
            uiManager.OpenScreen(mapScreen);
        }

        private void OpenPvP()
        {
            DependencyInjector.ReplaceComponent(BattleSetupFactory.Create(profile, enemySetups));
            LoadScene(battleSceneReference);
        }

        private void LoadScene(AssetReference sceneReference)
        {
            uiManager.OpenLoadingScreen(() => DependencyInjector.Resolve<ScenesLoader>().LoadScene(sceneReference));
        }
    }
}