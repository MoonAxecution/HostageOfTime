using HOT.Equipment;
using HOT.Inventory.Item;
using HOT.Skills;

namespace HOT.Creature
{
    public class Humanoid : Creature
    {
        private readonly Equipment.Equipment equipment;

        public Weapon Weapon => equipment.Weapon;
        public Skill[] WeaponSkills => equipment.WeaponSkills;
        public bool IsArmed => equipment.IsWeaponSet.Value;

        public Humanoid(int defaultHealth, Equipment.Equipment equipment) : base(defaultHealth)
        {
            this.equipment = equipment;
            health.UpdateMaxHealth(health.MaxHealth.Value + this.equipment.GetHealthModifier());
        }

        public int GetDamage()
        {
            return equipment.GetDamage();
        }

        public IEquipmentCellReadOnly GetEquipmentCell(EquipmentType type) => equipment.GetCell(type);
    }
}