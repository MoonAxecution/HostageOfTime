using System;
using HOT.Battle.UI;
using UnityEngine;
using UnityEngine.UI;

namespace HOT.Battle
{
    public class BattleCreatureCompositeRoot : CompositeRoot, ISelectable
    {
        [Header("Animator")] 
        [SerializeField] private Animator animator;
        public RuntimeAnimatorController unarmedController;
        public RuntimeAnimatorController riffleController;

        [Header("UI")] 
        [SerializeField] private BattleCreatureUI creatureUI;
        [SerializeField] private Image healthBar;

        //TODO: Переделать на эквипмент
        [SerializeField] private GameObject weapon;
        
        private Creature.Humanoid humanoid;

        private AttackStateMachine.AttackStateMachine attackStateMachine;

        public event Action AttackAnimationEnded;
        public event Action<BattleCreatureCompositeRoot> Died;

        public void Init(Equipment.Equipment equipment)
        {
            CreateCreature(equipment);
            
            animator.runtimeAnimatorController = humanoid.IsArmed ? riffleController : unarmedController;
            weapon.SetActive(humanoid.IsArmed);
            
            attackStateMachine = new AttackStateMachine.AttackStateMachine(animator);
            attackStateMachine.AttackAnimationEnded += OnAttackAnimationEnded;
        }

        private void CreateCreature(Equipment.Equipment equipment)
        {
            humanoid = new Creature.Humanoid(equipment);
            humanoid.Died += OnDied;
        }
        
        private void OnDied()
        {
            Died.Fire(this);
            Destroy(gameObject);
        }

        public void Attack()
        {
            attackStateMachine.StartAttack();
        }

        private void OnAttackAnimationEnded()
        {
            AttackAnimationEnded.Fire();
        }
        
        public void ApplyDamage(int damage)
        {
            humanoid.ApplyDamage(damage);
            healthBar.fillAmount = (float)humanoid.Health.CurrentHealth.Value / humanoid.Health.MaxHealth.Value;
        }

        public int GetDamage() => humanoid.GetDamage();

        public void Select()
        {
            creatureUI.ShowTargetSelector();
        }

        public void Unselect()
        {
            creatureUI.HideTargetSelector();
        }

        private void OnDestroy()
        {
            attackStateMachine.Dispose();
        }
    }
}