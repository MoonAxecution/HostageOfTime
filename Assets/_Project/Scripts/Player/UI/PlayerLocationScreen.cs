using UnityEngine;

namespace HOT.UI
{
    public class PlayerLocationScreen : Screen
    {
        [Inject] private Profile.Profile profile;
        
        [Header("UI")]
        [SerializeField] private TimeView timeView;
        [SerializeField] private JoystickView joystickView;

        public float VerticalInput => joystickView.JoystickAmount.y;
        public float HorizontalInput => joystickView.JoystickAmount.x;

        protected override void OnAwaken()
        {
            timeView.Init(profile.Time);
        }
    }
}