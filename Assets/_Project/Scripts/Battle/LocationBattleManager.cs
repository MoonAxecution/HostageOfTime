using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Battle
{
    public class LocationBattleManager : MonoBehaviour
    {
        [Inject] private ScenesLoader scenesLoader;
        
        [SerializeField] private AssetReference battleSceneReference;
        [SerializeField] private BattleInitiator battleInitiator;

        private GameObject enemy;
        
        private void Awake()
        {
            this.Inject();
            battleInitiator.EnemyFound += LoadBattle;
        }

        private void LoadBattle(GameObject enemy)
        {
            this.enemy = enemy;
            battleInitiator.gameObject.SetActive(false);

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
            Destroy(enemy);
        }

        private void OnBattleLost()
        {
            Destroy(enemy);
        }
    }
}