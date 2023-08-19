using System;
using HOT.Core.Reactive;
using HOT.Inventory.Item;
using HOT.Skills;
using UnityEngine;

namespace HOT.UI
{
    public class PlayerBattleScreen : Screen
    {
        [SerializeField] private TimeView turnTimerView;
        [SerializeField] private SkillButtonView[] skillButtons;

        private Weapon currentWeapon;
        
        public event Action AttackPressed;

        protected override void OnAwaken()
        {
            foreach (var skillButton in skillButtons)
                skillButton.Pressed += Attack;
        }

        public void SetTurnTimer(IReactiveProperty<int> timeProperty)
        {
            turnTimerView.Init(timeProperty);
        }

        public void SetSkills(Weapon weapon, Skill[] weaponSkills)
        {
            currentWeapon = weapon;
            int nextSkillButtonIndex = 0;

            for (; nextSkillButtonIndex < weaponSkills.Length; nextSkillButtonIndex++)
            {
                SkillButtonView skillButtonView = skillButtons[nextSkillButtonIndex];
                skillButtonView.Init(weaponSkills[nextSkillButtonIndex]);
                skillButtonView.Show();
            }
            
            for (; nextSkillButtonIndex < skillButtons.Length; nextSkillButtonIndex++)
                skillButtons[nextSkillButtonIndex].Hide();
        }
        
        private void Attack(Skill skill)
        {
            if (currentWeapon != null)
                currentWeapon.UseSkill(skill);
            
            AttackPressed.Fire();
        }
    }
}