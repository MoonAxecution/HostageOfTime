using System;
using HOT.Data;
using HOT.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Screen = HOT.UI.Screen;

namespace HOT
{
    public class ResourceTimeCompositeRoot : IDisposable
    {
        [Inject] private CrossSessionData crossSessionData;
        [Inject] private TickerMono tickerMono;
        [Inject] private UIManager uiManager;

        private readonly AssetReference deadMessageScreenAsset;
        
        private ResourceTimeModel model;

        public IResourceTimeModel Model => model;

        public ResourceTimeCompositeRoot(AssetReference deadMessageScreenAsset)
        {
            this.Inject();
            this.deadMessageScreenAsset = deadMessageScreenAsset;

            CreateModel();
        }
        
        private void CreateModel()
        {
            model = new ResourceTimeModel(crossSessionData.DieTime);
            model.TimeEnded += OnTimeEnded;
            tickerMono.Add(model.Timer);
        }

        public void Reset()
        {
            crossSessionData.DieTime = DateTime.Now.ToUnixTimestamp() + DateUtils.SecondsInDay;
            model.Reset(crossSessionData.DieTime);
            uiManager.UnlockInput();
        }

        private void OnTimeEnded()
        {
            ShowDeadMessage();
        }

        private async void ShowDeadMessage()
        {
            uiManager.LockInput();
            
            var screen = await uiManager.OpenScreen<Screen>(deadMessageScreenAsset);
            uiManager.UnlockScreen(screen);
        }

        public void Dispose()
        {
            tickerMono.Remove(model.Timer);
        }
    }
}