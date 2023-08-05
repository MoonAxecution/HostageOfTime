using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace HOT.UI
{
    public class JoystickView : MonoBehaviour, ITickable
    {
        [Inject] private TickerMono tickerMono;
        
        [Header("UI")]
        [SerializeField] private RectTransform joystickRect;
        [SerializeField] private RectTransform movablePointRect;
        [SerializeField] private Vector2 joystickSize;

        private Canvas canvas;
        private Vector2 originJoystickPosition;
        private Finger movementFinger;
        
        public Vector2 JoystickAmount { get; private set; }

        private void Awake()
        {
            this.Inject();
            
            tickerMono.Add(this);
            
            canvas = DependencyInjector.Resolve<UIManager>().Canvas;
            originJoystickPosition = joystickRect.transform.localPosition;
        }

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        private void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        public void Tick(float deltaTime)
        {
            if (Touch.activeFingers.Count < 1) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            Touch activeTouch = Touch.activeFingers[0].currentTouch;

            switch (activeTouch.phase)
            {
                case TouchPhase.Began:
                    OnFingerDown(activeTouch.finger);
                    break;
                case TouchPhase.Moved:
                    OnFingerMove(activeTouch.finger);
                    break;
                case TouchPhase.Ended:
                    OnFingerUp(activeTouch.finger);
                    break;
            }
        }

        private void OnFingerDown(Finger triggeredFinger)
        {
            if (movementFinger != null || triggeredFinger.screenPosition.x > UnityEngine.Screen.height / 2.0f) return;
            
            movementFinger = triggeredFinger;
            JoystickAmount = Vector2.zero;
            joystickRect.sizeDelta = joystickSize;
                
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                triggeredFinger.screenPosition, null, out Vector2 touchPosition);

            joystickRect.transform.localPosition = touchPosition;
        }

        private void OnFingerUp(Finger triggeredFinger)
        {
            if (triggeredFinger != movementFinger) return;

            movementFinger = null;
            joystickRect.transform.localPosition = originJoystickPosition;
            movablePointRect.anchoredPosition = Vector2.zero;
            JoystickAmount = Vector2.zero;
        }

        private void OnFingerMove(Finger triggeredFinger)
        {
            if (movementFinger != triggeredFinger) return;

            float maxMovement = joystickSize.x / 2.0f;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
                triggeredFinger.screenPosition, null, out Vector2 touchPosition);

            GetKnobPosition(touchPosition, maxMovement, out Vector2 knobPosition);

            movablePointRect.anchoredPosition = knobPosition;
            JoystickAmount = knobPosition / maxMovement;
        }

        private void GetKnobPosition(Vector2 touchPosition, float maxMovement, out Vector2 knobPosition)
        {
            if (Vector2.Distance(touchPosition, joystickRect.transform.localPosition) > maxMovement)
                knobPosition = (touchPosition - (Vector2)joystickRect.transform.localPosition).normalized * maxMovement;
            else
                knobPosition = touchPosition - (Vector2)joystickRect.transform.localPosition;
        }

        private void OnDestroy()
        {
            tickerMono.Remove(this);
        }
    }
}