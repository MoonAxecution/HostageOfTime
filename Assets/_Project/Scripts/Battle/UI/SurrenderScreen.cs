using System;
using UnityEngine;
using UnityEngine.UI;
using Screen = HOT.UI.Screen;

namespace HOT.Battle.UI
{
    public class SurrenderScreen : Screen
    {
        [SerializeField] private Button cancelButton;
        [SerializeField] private Button confirmButton;

        public event Action Confirmed;
        
        protected override void OnAwaken()
        {
            cancelButton.onClick.AddListener(CloseScreen);
            confirmButton.onClick.AddListener(Confirm);
        }

        private void Confirm()
        {
            CanvasGroup.interactable = false;
            Confirmed.Fire();
            CloseScreen();
        }
    }
}