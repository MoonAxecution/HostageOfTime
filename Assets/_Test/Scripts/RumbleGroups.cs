using System;
using HOT;
using UnityEngine;

public class RumbleGroups : MonoBehaviour
{
    public RumbleGroup[] groups;
    public int currentTime;

    private const int TimeBetweenGroups = 10;
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        int nextStartTime = DateTime.Now.ToUnixTimestamp();
        
        foreach (var group in groups)
        {
            group.Init(nextStartTime, nextStartTime + TimeBetweenGroups);
            nextStartTime += TimeBetweenGroups;
        }
    }

    private void Update()
    {
        foreach (var group in groups)
            group.Tick();
        
        int nextStartTime = currentTime = DateTime.Now.ToUnixTimestamp();

        for (int i = 0; i < groups.Length; i++)
        {
            RumbleGroup group = groups[i];
            
            if (group.IsSetStartTime)
            {
                if ((nextStartTime - group.startTime) >= TimeBetweenGroups)
                {
                    UpdateExpiredEventStartTime(i);
                }
            }
        }
    }
    
    private void UpdateExpiredEventStartTime(int currentEventIndex) 
    {
        RumbleGroup previousEvent = GetPreviousEvent(currentEventIndex);
        groups[currentEventIndex].Init(previousEvent.startTime + TimeBetweenGroups, previousEvent.startTime + TimeBetweenGroups + TimeBetweenGroups);
    }
    
    private RumbleGroup GetPreviousEvent(int currentEventIndex) {
        int previousEventIndex = currentEventIndex - 1;

        if (previousEventIndex < 0)
            previousEventIndex = groups.Length - 1;

        return groups[previousEventIndex];
    }
}
