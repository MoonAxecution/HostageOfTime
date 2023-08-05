using UnityEngine;

namespace HOT.Battle
{
    public class CreatureParametersView : MonoBehaviour
    {
        [SerializeField] private Transform gameCamera;

        private void Update()
        {
            transform.LookAt(transform.position + gameCamera.rotation * Vector3.forward, gameCamera.rotation * Vector3.up);
        }
    }
}