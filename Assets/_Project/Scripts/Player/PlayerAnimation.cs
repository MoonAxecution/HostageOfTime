using System;
using HOT.Core.Reactive;
using UnityEngine;

namespace HOT.Player
{
    public class PlayerAnimation : IDisposable
    {
        private const string MoveAmountParameter = "MoveAmount";
        private const string ArmedParameter = "Armed";
        
        private readonly Animator animator;

        private IReactiveProperty<bool> isWeaponSetProperty;

        public PlayerAnimation(Animator animator, IReactiveProperty<bool> isWeaponSetProperty)
        {
            this.animator = animator;
            
            this.isWeaponSetProperty = isWeaponSetProperty;
            isWeaponSetProperty.Changed += UpdateArmedState;

            UpdateArmedState(this.isWeaponSetProperty.Value);
        }

        public void Update(float moveAmount)
        {
            animator.SetFloat(MoveAmountParameter, moveAmount);
        }

        private void UpdateArmedState(bool state)
        {
            animator.SetBool(ArmedParameter, state);
        }

        public void Dispose()
        {
            isWeaponSetProperty.Changed -= UpdateArmedState;
        }
    }
}