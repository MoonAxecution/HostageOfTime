using System;
using HOT.Core.Reactive;
using HOT.Data;

namespace HOT
{
    public interface IResourceTimeModel
    {
        void IncreaseTime(int time);
        Timer Timer { get; }
        IReactiveProperty<int> Time { get; }
    }
    
    public class ResourceTimeModel : IResourceTimeModel
    {
        [Inject] private CrossSessionData crossSessionData;

        private readonly Timer timer;

        public Timer Timer => timer;
        public IReactiveProperty<int> Time => timer.LeftTime;

        public event Action TimeEnded;

        public ResourceTimeModel(int time)
        {
            this.Inject();
            
            timer = new Timer(time);
            timer.TimerEnded += OnTimerEnded;
        }

        public void Reset(int value)
        {
            timer.Reset(value);
        }
        
        public void IncreaseTime(int value)
        {
            timer.IncreaseTime(value);
        }

        private void OnTimerEnded()
        {
            TimeEnded.Fire();
        }
    }
}