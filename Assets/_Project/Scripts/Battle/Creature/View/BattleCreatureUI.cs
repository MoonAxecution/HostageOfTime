using UnityEngine;

namespace HOT.Battle.UI
{
    public class BattleCreatureUI : MonoBehaviour, ITickable
    {
        [Inject] private TickerMono tickerMono;
        
        [SerializeField] private GameObject targetSelector;
        [SerializeField] private float targetSelectorRotationSpeed;

        private void Awake()
        {
            this.Inject();
            tickerMono.Add(this);
        }

        public void Tick(float deltaTime)
        {
            targetSelector.transform.Rotate(-Vector3.forward * targetSelectorRotationSpeed * deltaTime);
        }
        
        public void ShowTargetSelector()
        {
            targetSelector.SetActive(true);
        }

        public void HideTargetSelector()
        {
            targetSelector.SetActive(false);
        }

        private void OnDestroy()
        {
            tickerMono.Remove(this);
        }
    }
}