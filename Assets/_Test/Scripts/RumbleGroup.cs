using System;
using HOT;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RumbleGroup : MonoBehaviour
{
    public Image background;
    public TMP_Text leftTimeText;
    
    public int startTime;
    private int nextGroupStartTime;

    public bool IsSetStartTime => startTime > 0;
    
    public void Init(int startTime, int nextGroupStartTime)
    {
        this.startTime = startTime;
        this.nextGroupStartTime = nextGroupStartTime;
    }

    public void Tick()
    {
        background.color = startTime < DateTime.Now.ToUnixTimestamp() ? Color.white : Color.gray;

        TimeSpan timeSpan;
        
        if (startTime < DateTime.Now.ToUnixTimestamp())
            timeSpan = GetLeftTimeSpan(nextGroupStartTime - DateTime.Now.ToUnixTimestamp());
        else
            timeSpan = GetLeftTimeSpan(startTime - DateTime.Now.ToUnixTimestamp());

        leftTimeText.text = $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
    }

    private TimeSpan GetLeftTimeSpan(int value) => TimeSpan.FromSeconds(value);
}
