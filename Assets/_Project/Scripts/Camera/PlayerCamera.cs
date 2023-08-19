using UnityEngine;

namespace HOT.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;

        private void LateUpdate()
        {
            if (target == null) return;
            
            transform.position = target.position + offset;
        }

        public void SetNewTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}