using System;
using HOT.Core.Reactive;

namespace HOT
{
    public class Timer : ITickable
    {
        private readonly ReactiveProperty<int> leftTime;

        private int endTime;
        private bool isStoped;
        
        public IReactiveProperty<int> LeftTime => leftTime;
        
        public event Action TimerEnded;

        public Timer(bool isStoped = false)
        {
            this.isStoped = isStoped;
            leftTime = new ReactiveProperty<int>(0);
        }
        
        public Timer(int endTime, bool isStoped = false)
        {
            this.isStoped = isStoped;
            this.endTime = endTime;
            leftTime = new ReactiveProperty<int>(GetLeftTime());
        }

        public void Reset(int value)
        {
            endTime = value;
            UpdateLeftTime();
            isStoped = false;
        }

        public void StopTimer()
        {
            isStoped = true;
        }

        public void Tick(float deltaTime)
        {
            if (isStoped) return;
            
            CalculateLeftTime();
        }
        
        public void IncreaseTime(int time)
        {
            endTime += time;
            UpdateLeftTime();
        }
        
        public void CalculateLeftTime()
        {
            UpdateLeftTime();
            
            if (leftTime.Value > 0) return;

            leftTime.Value = 0;
            isStoped = true;
            TimerEnded.Fire();
        }
        
        private void UpdateLeftTime()
        {
            this.leftTime.Value = GetLeftTime();
        }

        private int GetLeftTime() => endTime - DateTime.Now.ToUnixTimestamp();
    }
}