using System.Collections.Generic;
using UnityEngine;

namespace HOT
{
    public class TickerMono : MonoBehaviour
    {
        private readonly HashSet<ITickable> tickables = new HashSet<ITickable>();
        private readonly Queue<ITickable> addingTickables = new Queue<ITickable>();
        private readonly Queue<ITickable> removingTickables = new Queue<ITickable>();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        private void Update()
        {
            ValidateTickables();
            
            float deltaTime = UnityEngine.Time.deltaTime;
            
            foreach (ITickable tickable in tickables)
                tickable.Tick(deltaTime);
        }

        private void ValidateTickables()
        {
            foreach (ITickable tickable in removingTickables)
                tickables.Remove(tickable);
            
            removingTickables.Clear();

            foreach (ITickable tickable in addingTickables)
                tickables.Add(tickable);
            
            addingTickables.Clear();
        }
        
        public void Add(ITickable tickable)
        {
            addingTickables.Enqueue(tickable);
        }

        public void Remove(ITickable tickable)
        {
            removingTickables.Enqueue(tickable);
        }
    }
}