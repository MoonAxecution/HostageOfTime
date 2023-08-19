using HOT.Inventory.Item;
using HOT.Skills;

namespace HOT.Equipment
{
    public class EquipedWeaponManager
    {
        private IEquipmentCellReadOnly weaponCell;
        private Weapon equipedWeapon;
        
        private Skill defaultSkill;

        public Weapon EquipedWeapon => equipedWeapon;
        public Skill[] Skills => equipedWeapon != null ? equipedWeapon.Skills : new[] {defaultSkill};

        public EquipedWeaponManager(IEquipmentCellReadOnly weaponCell)
        {
            this.weaponCell = weaponCell;
            this.weaponCell.Equiped += OnWeaponEquiped;
            this.weaponCell.TookOff += OnWeaponTookOff;

            SetWeapon(this.weaponCell.Item as Weapon);
            
            defaultSkill = new Skill(SkillTargetType.Solo, 3, 6);
        }

        private void OnWeaponEquiped()
        {
            SetWeapon(weaponCell.Item as Weapon);
        }

        private void OnWeaponTookOff()
        {
            SetWeapon(null);
        }

        private void SetWeapon(Weapon weapon)
        {
            equipedWeapon = weapon;
        }

        public int GetDamage()
        {
            return equipedWeapon?.GetDamage() ?? defaultSkill.GetDamage();
        }
    }
}