using UnityEngine;

namespace HOT.UI
{
    public class PlayerLocationScreen : Screen
    {
        [SerializeField] private JoystickView joystickView;

        public float VerticalInput => joystickView.JoystickAmount.y;
        public float HorizontalInput => joystickView.JoystickAmount.x;
    }
}