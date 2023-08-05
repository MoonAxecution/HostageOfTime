using System;
using HOT.Core.Reactive;

namespace HOT.Battle
{
    public class Battle : ITickable
    {
        private readonly Timer timer;

        public IReactiveProperty<int> Time => timer.LeftTime;
        
        public event Action TimeEnded;

        public Battle(int playerTurnTime)
        {
            timer = new Timer(DateTime.Now.ToUnixTimestamp() + playerTurnTime);
            timer.TimerEnded += OnTimerEnded;
        }

        public void ResetTime(int value)
        {
            timer.Reset(DateTime.Now.ToUnixTimestamp() + value);
        }
        
        public void Tick(float deltaTime)
        {
            timer.CalculateLeftTime();
        }

        private void OnTimerEnded()
        {
            TimeEnded.Fire();
        }
    }
}