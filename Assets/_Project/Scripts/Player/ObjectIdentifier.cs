using System;
using HOT.Creature;
using UnityEngine;

namespace HOT
{
    public class ObjectIdentifier : MonoBehaviour
    {
        private IActable currentActableObject;
        
        public event Action<LocationEnemyCompositeRoot> EnemyFound;
        public event Action<IActable> ActableObjectFound;
        public event Action ActableObjectLost;
        
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<LocationEnemyCompositeRoot>();

            if (enemy != null)
            {
                EnemyFound.Fire(enemy);
                return;
            }

            var actableObject = other.GetComponent<IActable>();

            if (actableObject != null)
            {
                currentActableObject = actableObject;
                ActableObjectFound.Fire(actableObject);
                return;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var actableObject = other.GetComponent<IActable>();
            
            if (actableObject != null && actableObject != currentActableObject) return;

            currentActableObject = null;
            ActableObjectLost.Fire();
        }
    }
}