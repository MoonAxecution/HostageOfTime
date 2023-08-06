using HOT.Core.Reactive;

namespace HOT.Components
{
    public interface IHealth
    {
        IReactiveProperty<int> CurrentHealth { get; }
        IReactiveProperty<int> MaxHealth { get; }
    }
    
    public class Health : IHealth
    {
        private readonly ReactiveProperty<int> currentHealth;
        private readonly ReactiveProperty<int> maxHealth;

        private const int StaticHealth = 50;

        public IReactiveProperty<int> CurrentHealth => currentHealth;
        public IReactiveProperty<int> MaxHealth => maxHealth;

        public Health()
        {
            currentHealth = new ReactiveProperty<int>(StaticHealth);
            maxHealth = new ReactiveProperty<int>(StaticHealth);
        }

        public void UpdateMaxHealth(int value)
        {
            maxHealth.Value = value;
            currentHealth.Value = maxHealth.Value;
        }
        
        public void Increase(int value)
        {
            currentHealth.Value += value;
        }

        public void Decrease(int value)
        {
            currentHealth.Value -= value;
        }
    }
}