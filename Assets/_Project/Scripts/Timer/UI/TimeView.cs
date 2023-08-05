using System;
using HOT.Core.Reactive;
using TMPro;
using UnityEngine;

namespace HOT.UI
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeLabel;
        [SerializeField] private Gradient labelColor;
        [SerializeField] private bool showHours;
        
        private IReactiveProperty<int> time;

        public void Init(IReactiveProperty<int> timeProperty)
        {
            time = timeProperty;
            time.Changed += SetTime;
        }

        private void SetTime(int value)
        {
            var timeSpan = TimeSpan.FromSeconds(value);
            
            timeLabel.text = showHours 
                ? $"{(int)timeSpan.TotalHours}{timeSpan.Minutes::00}:{timeSpan.Seconds:00}" 
                : $"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
            
            UpdateTimeColor((float)timeSpan.TotalSeconds);
        }

        private void UpdateTimeColor(float value)
        {
            float colorTime = Mathf.Clamp(value, 0.0f, DateUtils.SecondsInDay) / DateUtils.SecondsInDay;
            timeLabel.color = labelColor.Evaluate(colorTime);
        }

        private void OnDestroy()
        {
            if (time == null) return;
            
            time.Changed -= SetTime;
        }
    }
}