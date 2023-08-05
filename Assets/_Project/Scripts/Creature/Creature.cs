using System;
using HOT.Components;

namespace HOT.Creature
{
    public class Creature
    {
        private readonly Health health;
        
        public IHealth Health => health;
        
        public event Action Died;

        public Creature()
        {
            health = new Health();
            health.CurrentHealth.Changed += OnHealthChanged;
        }

        public void ApplyDamage(int value)
        {
            health.Decrease(value);
        }

        private void OnHealthChanged(int value)
        {
            if (value > 0) return;
            
            Died.Fire();
        }
    }
}