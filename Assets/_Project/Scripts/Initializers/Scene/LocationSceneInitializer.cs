using System;
using HOT.Addressable;
using HOT.Battle;
using HOT.Player;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT
{
    public class LocationSceneInitializer : SceneInitializer
    {
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private AssetReference playerAsset;
        [SerializeField] private LocationBattleManager locationBattleManager;

        private PlayerCompositeRoot playerCompositeRoot;
        
        protected override void OnAwaken()
        {
            CreatePlayer();
        }

        private async void CreatePlayer()
        {
            playerCompositeRoot = await AddressableAssetLoader.LoadInstantiatableAsset<PlayerCompositeRoot>(playerAsset, playerSpawnPoint);
            playerCompositeRoot.SetPlayerCamera(playerCamera.transform);
            
            playerCamera.SetNewTarget(playerCompositeRoot.transform);
            locationBattleManager.Init(playerCompositeRoot.ObjectIdentifier);
        }

        public void ResetPlayerPosition()
        {
            playerCompositeRoot.transform.localPosition = Vector3.zero;
            playerCompositeRoot.transform.rotation = playerSpawnPoint.rotation;
        }

        private void OnDestroy()
        {
            if (playerCompositeRoot == null) return;
            
            AddressableAssetLoader.UnloadAsset(playerCompositeRoot.gameObject);
        }
    }
}