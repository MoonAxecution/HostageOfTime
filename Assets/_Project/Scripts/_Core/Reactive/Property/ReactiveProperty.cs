using System;

namespace HOT.Core.Reactive
{
    public interface IReactiveProperty<TType>
    {
        TType Value { get; set; }

        event Action<TType> Changed;
    }
    
    public class ReactiveProperty<TType> : IReactiveProperty<TType>
    {
        private TType value;
        
        public TType Value
        {
            get { return value; }

            set
            {
                SetValue(value);
            }
            
        }
        
        public event Action<TType> Changed;

        public ReactiveProperty() : this(default) {}

        public ReactiveProperty(TType originValue)
        {
            SetValue(originValue);
        }

        private void SetValue(TType value)
        {
            this.value = value;
            Changed.Fire(this.value);
        }
    }
}