using HOT.Creature;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;

namespace HOT.Battle
{
    public class LocationBattleManager : MonoBehaviour
    {
        [Inject] private Profile.Profile profile;
        [Inject] private ScenesLoader scenesLoader;

        [SerializeField] private LocationSceneInitializer locationSceneInitializer;
        [SerializeField] private AssetReference battleSceneReference;

        private ObjectIdentifier objectIdentifier;
        private BattleSetup battleSetup;
        private LocationEnemyCompositeRoot enemy;

        public void Init(ObjectIdentifier objectIdentifier)
        {
            this.Inject();
            
            this.objectIdentifier = objectIdentifier;
            objectIdentifier.EnemyFound += LoadBattle;
        }

        private void LoadBattle(LocationEnemyCompositeRoot enemy)
        {
            this.enemy = enemy;
            objectIdentifier.gameObject.SetActive(false);

            DependencyInjector.ReplaceComponent(BattleSetupFactory.Create(profile, enemy.EnemySetups));
            
            scenesLoader.SceneLoaded += OnBattleSceneLoaded;
            scenesLoader.LoadSceneWithLoadingScreen(battleSceneReference, true);
        }

        private void OnBattleSceneLoaded()
        {
            scenesLoader.SceneLoaded -= OnBattleSceneLoaded;
            
            var battleManager = FindObjectOfType<BattleManager>();
            battleManager.Won += OnBattleWon;
            battleManager.Lost += OnBattleLost;
        }

        private void OnBattleWon()
        {
            Destroy(enemy.gameObject);
            OnBattleEnded();
        }

        private void OnBattleLost()
        {
            locationSceneInitializer.ResetPlayerPosition();
            OnBattleEnded();
        }
        
        private void OnBattleEnded()
        {
            objectIdentifier.gameObject.SetActive(true);
        }
    }
}