namespace HOT.Creature
{
    public class Humanoid : Creature
    {
        private readonly Equipment.Equipment equipment;
        
        public bool IsArmed => equipment.IsWeaponSet;

        public Humanoid(Equipment.Equipment equipment)
        {
            this.equipment = equipment;
            health.UpdateMaxHealth(health.MaxHealth.Value + this.equipment.GetHealthModifier());
        }

        public int GetDamage()
        {
            return equipment.GetDamage();
        }
    }
}