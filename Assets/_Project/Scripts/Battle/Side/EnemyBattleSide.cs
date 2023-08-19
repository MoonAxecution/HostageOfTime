using HOT.Creature;
using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.Battle
{
    public class EnemyBattleSide : BattleSide
    {
        [SerializeField] private BattleCreatureCompositeRoot creaturePrefab;
        [SerializeField] private WeaponSettings weapon;
        
        public void CreateSide(Humanoid[] allies)
        {
            SpawnCreatures(creaturePrefab, allies.Length);
            CreateAllies(allies);
        }
        
        protected override void CreateAllies(Humanoid[] allies)
        {
            for (int i = 0; i < allies.Length; i++)
            {
                Allies[i].Init(allies[i]);
                Allies[i].Died += OnAllyDied;
            }
        }

        protected override BattleCreatureCompositeRoot GetAttaackableEnemy() => Enemies[Random.Range(0, Enemies.Count)];
    }
}