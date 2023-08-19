using HOT.Equipment;
using HOT.Skills;

namespace HOT.Inventory.Item
{
    public class Weapon : Equipment
    {
        public Skill[] Skills { get; private set; }

        private Skill selectedSkill;
        
        public Weapon(ItemType type, int id, string name, EquipmentType equipmentType, Skill[] skills) 
            : base(type, id, name, equipmentType)
        {
            Skills = skills;
            UseSkill(skills[0]);
        }

        public void UseSkill(Skill skill)
        {
            selectedSkill = skill;
        }

        public int GetDamage() => selectedSkill.GetDamage();
    }
}