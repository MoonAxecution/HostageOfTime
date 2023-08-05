using System;
using UnityEngine;

namespace HOT.Battle.AttackStateMachine
{
    public class AttackStateMachine : IDisposable
    {
        private readonly Animator animator;
        private readonly string startAttackTrigger = "Attack";
        private readonly AttackAnimationBehaviour attackAnimationBehaviour;

        public event Action AttackAnimationEnded;
        
        public AttackStateMachine(Animator animator)
        {
            this.animator = animator;
            attackAnimationBehaviour = this.animator.GetBehaviour<AttackAnimationBehaviour>();
            attackAnimationBehaviour.AttackEnded += EndAttack;
        }

        public void StartAttack()
        {
            animator.SetTrigger(startAttackTrigger);
        }

        private void EndAttack()
        {
            AttackAnimationEnded.Fire();
        }

        public void Dispose()
        {
            attackAnimationBehaviour.AttackEnded -= EndAttack;
        }
    }
}