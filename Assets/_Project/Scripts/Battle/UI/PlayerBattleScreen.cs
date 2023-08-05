using System;
using HOT.Core.Reactive;
using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI
{
    public class PlayerBattleScreen : Screen
    {
        [SerializeField] private TimeView timeView;
        [SerializeField] private Button attackButton;

        public event Action AttackPressed;
        
        protected override void OnAwaken()
        {
            attackButton.onClick.AddListener(Attack);
        }

        public void Init(IReactiveProperty<int> timeProperty)
        {
            timeView.Init(timeProperty);
        }
        
        private void Attack()
        {
            AttackPressed.Fire();
        }
    }
}