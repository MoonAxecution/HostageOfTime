using System;
using UnityEngine;

namespace HOT.Player
{
    public class PlayerInput : IDisposable
    {
        private readonly PlayerControls playerControls;
        
        private Vector2 movement;
        
        public float VerticalInput { get; private set; }
        public float HorizontalInput { get; private set; }

        public PlayerInput()
        {
            playerControls = new PlayerControls();
            playerControls.Movement.Movement.performed += i => movement = i.ReadValue<Vector2>();
            playerControls.Enable();
        }

        public void Update()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            VerticalInput = movement.y;
            HorizontalInput = movement.x;
        }
        
        public void Dispose()
        {
            playerControls?.Dispose();
        }
    }
}