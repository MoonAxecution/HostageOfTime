using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HOT.Battle
{
    public abstract class BattleSide : MonoBehaviour
    {
        [SerializeField] private Transform[] allySpawnPoints;
        [SerializeField] private List<BattleCreatureCompositeRoot> allies;

        private List<BattleCreatureCompositeRoot> enemies;
        
        private List<int> atackedAllies = new List<int>();
        private int lastAllyIdTurn;
        
        public event Action TurnMade;

        protected List<BattleCreatureCompositeRoot> Enemies => enemies;

        public List<BattleCreatureCompositeRoot> Allies => allies;

        protected void SpawnCreatures(BattleCreatureCompositeRoot creaturePrefab, int count)
        {
            for (int i = 0; i < count; i++)
                SpawnCreature(creaturePrefab, allySpawnPoints[i]);
        }

        private void SpawnCreature(BattleCreatureCompositeRoot creaturePrefab, Transform spawnPoint)
        {
            BattleCreatureCompositeRoot creature = Instantiate(creaturePrefab, spawnPoint);
            creature.gameObject.SetActive(true);
            allies.Add(creature);
        }

        protected abstract void CreateAllies();

        protected void OnAllyDied(BattleCreatureCompositeRoot ally)
        {
            ally.Died -= OnAllyDied;
            allies.Remove(ally);
        }

        public virtual void SetEnemies(List<BattleCreatureCompositeRoot> enemies)
        {
            this.enemies = enemies;

            foreach (BattleCreatureCompositeRoot enemy in this.enemies)
                enemy.Died += OnEnemyDied;
        }

        protected virtual void OnEnemyDied(BattleCreatureCompositeRoot enemy)
        {
            enemy.Died -= OnEnemyDied;
            enemies.Remove(enemy);
        }

        public virtual void StartAttack()
        {
            allies[lastAllyIdTurn].AttackAnimationEnded += EndAttack;
            allies[lastAllyIdTurn].Attack();
        }

        private void EndAttack()
        {
            BattleCreatureCompositeRoot currentAlly = allies[lastAllyIdTurn];
            
            currentAlly.AttackAnimationEnded -= EndAttack;
            GetAttaackableEnemy().ApplyDamage(currentAlly.GetDamage());

            StoreAllyTurn();

            if (CanNextAllyAttack())
            {
                StartAttack();
                return;
            }

            ClearTurn();
            InvokeTurnMadeEvent();
        }

        protected abstract BattleCreatureCompositeRoot GetAttaackableEnemy();

        private void StoreAllyTurn()
        {
            atackedAllies.Add(lastAllyIdTurn);
            lastAllyIdTurn++;
        }

        private void ClearTurn()
        {
            atackedAllies.Clear();
            lastAllyIdTurn = 0;
        }

        private bool CanNextAllyAttack() => enemies.Count > 0 && atackedAllies.Count != allies.Count;

        protected void InvokeTurnMadeEvent()
        {
            TurnMade.Fire();
        }
    }
}