using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI
{
    public class DeathMessageScreen : Screen
    {
        [SerializeField] private Button restartButton;

        protected override void OnAwaken()
        {
            restartButton.onClick.AddListener(Restart);
        }

        private void Restart()
        {
            DependencyInjector.Resolve<ResourceTimeCompositeRoot>().Reset();
            CloseScreen();
        }
    }
}