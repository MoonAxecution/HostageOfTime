using System;
using HOT.Battle.UI;
using HOT.Equipment;
using HOT.Inventory.Item;
using HOT.Player;
using HOT.Skills;
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
        [SerializeField] private EquipmentView equipmentView;
        [SerializeField] private Image healthBar;

        private Creature.Humanoid humanoid;

        private AttackStateMachine.AttackStateMachine attackStateMachine;

        public Weapon Weapon => humanoid.Weapon;
        public Skill[] WeaponSkills => humanoid.WeaponSkills;

        public event Action AttackAnimationEnded;
        public event Action<BattleCreatureCompositeRoot> Died;

        public void Init(Creature.Humanoid humanoid)
        {
            this.humanoid = humanoid;
            humanoid.Died += OnDied;
            
            equipmentView.Init(humanoid.GetEquipmentCell(EquipmentType.Weapon), humanoid.GetEquipmentCell(EquipmentType.Helmet));

            animator.runtimeAnimatorController = humanoid.IsArmed ? riffleController : unarmedController;

            attackStateMachine = new AttackStateMachine.AttackStateMachine(animator);
            attackStateMachine.AttackAnimationEnded += OnAttackAnimationEnded;
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