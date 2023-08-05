using System;
using HOT.Battle.UI;
using HOT.Core.Reactive;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HOT.UI
{
    public class BattleScreen : Screen
    {
        [Header("Assets")]
        [SerializeField] private AssetReference surrenderScreenAsset;
        
        [Header("UI")]
        [SerializeField] private Button surrenderButton;

        public Action Surrendered;
        
        protected override void OnAwaken()
        {
            surrenderButton.onClick.AddListener(OpenSurrenderScreen);
        }

        private async void OpenSurrenderScreen()
        {
            var screen = await uiManager.OpenScreen<SurrenderScreen>(surrenderScreenAsset);
            screen.Confirmed += OnSurrendered;
        }

        private void OnSurrendered()
        {
            Surrendered.Fire();
        }

        public void DisableScreenInput()
        {
            CanvasGroup.interactable = false;
        }
    }
}