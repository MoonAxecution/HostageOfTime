using UnityEngine;

namespace HOT.Player
{
    public class PlayerLocomotion
    {
        private readonly CharacterController characterController;
        private readonly Transform transform;
        
        private Vector3 moveDirection;

        public PlayerLocomotion(CharacterController characterController, Transform transform)
        {
            this.characterController = characterController;
            this.transform = transform;
        }
        
        public void Update(Transform direction, float verticalMovement, float horizontalMovement)
        {
            SetMoveDirection(direction, verticalMovement, horizontalMovement);
            Move();
            Rotate();
        }

        private void Move()
        {
            characterController.Move(moveDirection * 5 * UnityEngine.Time.deltaTime);
        }

        private void SetMoveDirection(Transform direction, float verticalMovement, float horizontalMovement)
        {
            Vector3 forwardVector = Vector3.ProjectOnPlane(direction.forward, Vector3.up).normalized;
            Vector3 rightVector = direction.right;
            moveDirection = forwardVector * verticalMovement + rightVector * horizontalMovement;
        }

        private void Rotate()
        {
            if (moveDirection == Vector3.zero)
                moveDirection = transform.forward;

            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 5 * UnityEngine.Time.deltaTime);
        }
    }
}