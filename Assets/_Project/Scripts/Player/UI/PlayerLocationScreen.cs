using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace HOT.UI
{
    public class PlayerLocationScreen : Screen
    {
        [Inject] private Profile.Profile profile;
        
        [Header("Screens")]
        [SerializeField] private AssetReference playerInfoScreen;
        
        [Header("UI")]
        [SerializeField] private TimeView timeView;
        [SerializeField] private JoystickView joystickView;
        
        [Header("Buttons")]
        [SerializeField] private Button playerInfoButton;
        [SerializeField] private Button actionButton;
        
        public event Action Acted;
        
        public float VerticalInput => joystickView.JoystickAmount.y;
        public float HorizontalInput => joystickView.JoystickAmount.x;

        protected override void OnAwaken()
        {
            playerInfoButton.onClick.AddListener(OpenPlayerInfo);
            actionButton.onClick.AddListener(Act);
            
            timeView.Init(profile.Time);
        }
        
        private void OpenPlayerInfo()
        {
            uiManager.OpenScreen(playerInfoScreen);
        }

        private void Act()
        {
            Acted.Fire();
        }

        public void ShowActButton()
        {
            actionButton.gameObject.SetActive(true);
        }

        public void HideActButton()
        {
            actionButton.gameObject.SetActive(false);
        }
    }
}