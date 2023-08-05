using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.Battle
{
    public class EnemyBattleSide : BattleSide
    {
        [SerializeField] private BattleCreatureCompositeRoot creaturePrefab;
        [SerializeField] private WeaponSettings weapon;
        
        public void CreateSide()
        {
            SpawnCreatures(creaturePrefab, Random.Range(1, 4));
            CreateAllies();
        }
        
        protected override void CreateAllies()
        {
            foreach (var ally in Allies)
            {
                ally.Init(GetRandomEquipment());
                ally.Died += OnAllyDied;
            }
        }

        private Equipment.Equipment GetRandomEquipment()
        {
            var equipment = new Equipment.Equipment();
            equipment.EquipWeapon(Random.Range(0, 2) != 0 ? ItemSettingsToItemConverter.GetItem(weapon) : null);

            return equipment;
        }

        protected override BattleCreatureCompositeRoot GetAttaackableEnemy() => Enemies[Random.Range(0, Enemies.Count)];
    }
}