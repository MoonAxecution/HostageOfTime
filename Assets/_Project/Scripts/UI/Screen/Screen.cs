using System;
using UnityEngine;

namespace HOT.UI
{
    public class Screen : MonoBehaviour
    {
        [Inject] protected UIManager uiManager;
        
        [SerializeField] private CanvasGroup canvasGroup;

        public event Action Closed;

        protected CanvasGroup CanvasGroup => canvasGroup;
        
        private void Awake()
        {
            this.Inject();
            
            OnAwaken();
        }

        protected virtual void OnAwaken() {}

        public void ShowScreen()
        {
            gameObject.SetActive(true);
        }
        
        public void HideScreen()
        {
            gameObject.SetActive(false);
        }

        public void CloseScreen()
        {
            uiManager.CloseScreen(this);
            Closed.Fire();
        }
    }
}