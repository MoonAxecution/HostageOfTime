using UnityEngine;

namespace HOT.Player
{
    public class PlayerAnimation
    {
        private const string MoveAmountParameter = "MoveAmount";
        private const string ArmedParameter = "Armed";
        
        private readonly Animator animator;
        
        public PlayerAnimation(Animator animator)
        {
            this.animator = animator;
        }

        public void Update(float moveAmount)
        {
            animator.SetFloat(MoveAmountParameter, moveAmount);
        }

        public void UpdateArmedState(bool state)
        {
            animator.SetBool(ArmedParameter, state);
        }
    }
}