using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.Creature
{
    public class LocationEnemyCompositeRoot : MonoBehaviour
    {
        [SerializeField] private EnemyBattleSetup[] enemySetups;

        public EnemyBattleSetup[] EnemySetups => enemySetups;
    }
}