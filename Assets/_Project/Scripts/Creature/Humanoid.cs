using UnityEngine;

namespace HOT.Creature
{
    public class Humanoid : Creature
    {
        private readonly Equipment.Equipment equipment;
        
        public bool IsArmed => equipment.IsWeaponSet;

        public Humanoid(Equipment.Equipment equipment)
        {
            this.equipment = equipment;
        }

        public int GetDamage()
        {
            return IsArmed ? equipment.GetDamageModifier() : Random.Range(3, 6);
        }
    }
}