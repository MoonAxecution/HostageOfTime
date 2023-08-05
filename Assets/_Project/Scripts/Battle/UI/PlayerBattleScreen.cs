using System;
using HOT.Core.Reactive;
using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI
{
    public class PlayerBattleScreen : Screen
    {
        [SerializeField] private TimeView turnTimerView;
        [SerializeField] private Button attackButton;

        public event Action AttackPressed;
        
        protected override void OnAwaken()
        {
            attackButton.onClick.AddListener(Attack);
        }

        public void Init(IReactiveProperty<int> timeProperty)
        {
            turnTimerView.Init(timeProperty);
        }
        
        private void Attack()
        {
            AttackPressed.Fire();
        }
    }
}