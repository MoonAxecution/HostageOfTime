using System;
using HOT.Components;

namespace HOT.Creature
{
    public class Creature
    {
        protected readonly Health health;
        
        public event Action Died;

        public IHealth Health => health;

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