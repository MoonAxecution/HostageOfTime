using HOT.Creature;
using HOT.Inventory.Item;

namespace HOT.Battle
{
    public static class BattleSetupFactory
    {
        private const int DefaultPlayerHealth = 30;
        
        public static BattleSetup Create(Profile.Profile profile, EnemyBattleSetup[] enemySetups)
        {
            Humanoid[] allies = GetAlliesForBattle(profile);
            Humanoid[] enemies = GetEnemiesForBattle(enemySetups);

            return new BattleSetup(allies, enemies);
        }

        private static Humanoid[] GetAlliesForBattle(Profile.Profile profile)
        {
            var allies = new Humanoid[1];
            allies[0] = new Humanoid(DefaultPlayerHealth, profile.Equipment);

            return allies;
        }
        
        private static Humanoid[] GetEnemiesForBattle(EnemyBattleSetup[] enemySetups)
        {
            var battleEnemies = new Humanoid[enemySetups.Length];

            for (int i = 0; i < battleEnemies.Length; i++)
            {
                EnemyBattleSetup enemyBattleSetup = enemySetups[i];
                battleEnemies[i] = new Humanoid(enemyBattleSetup.Health, GetEquipment(enemyBattleSetup));
            }

            return battleEnemies;
        }

        private static Equipment.Equipment GetEquipment(EnemyBattleSetup enemySetup)
        {
            Equipment.Equipment equipment = new Equipment.Equipment();
            
            foreach (EquipmentSettings equipmentSettings in enemySetup.Equipment)
                equipment.Equip(ItemSettingsToItemConverter.GetEquipmentItem(equipmentSettings));

            return equipment;
        }
    }
}