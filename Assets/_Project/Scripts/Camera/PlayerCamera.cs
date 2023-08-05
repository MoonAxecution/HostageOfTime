using UnityEngine;

namespace HOT.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }
    }
}