using System;
using HOT.Creature;
using UnityEngine;

namespace HOT.Battle
{
    public class BattleInitiator : MonoBehaviour
    {
        public event Action<GameObject> EnemyFound;
        
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.GetComponent<Enemy>();
            
            if (enemy == null) return;
            
            EnemyFound.Fire(enemy.gameObject);
        }
    }
}