using System;
using UnityEngine;

namespace HOT
{
    public class TargetSelector : MonoBehaviour, ITickable
    {
        [SerializeField] private Camera gameCamera;

        private ISelectable lastSelectedTarget;
        
        public event Action<ISelectable> TargetSelected;

        private void Awake()
        {
            GetTickerMono().Add(this);
        }

        public void SetDefaultSelectedTarget(ISelectable target)
        {
            lastSelectedTarget = target;
            lastSelectedTarget.Select();
        }
        
        public void Tick(float deltaTime)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            if (Physics.Raycast(GetRay(), out RaycastHit hit))
                HandleSelectedObject(hit);
        }

        private Ray GetRay() => gameCamera.ScreenPointToRay(Input.mousePosition);

        private void HandleSelectedObject(RaycastHit hit)
        {
            if (hit.collider == null) return;

            var selectedTarget = hit.collider.GetComponent<ISelectable>();
            
            if (selectedTarget == null) return;
            
            SetLastSelectedTarget(selectedTarget);
            TargetSelected.Fire(selectedTarget);
        }

        private void SetLastSelectedTarget(ISelectable selectedTarget)
        {
            lastSelectedTarget?.Unselect();

            lastSelectedTarget = selectedTarget;
            lastSelectedTarget.Select();
        }

        private void OnDestroy()
        {
            GetTickerMono().Remove(this);
        }

        private TickerMono GetTickerMono() => DependencyInjector.Resolve<TickerMono>();
    }
}