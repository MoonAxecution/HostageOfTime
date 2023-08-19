using System;
using HOT.Inventory.Item;
using UnityEngine;

namespace HOT.Creature
{
    [Serializable]
    public class EnemyBattleSetup
    {
        [SerializeField] private int health;
        [SerializeField] private EquipmentSettings[] equipment;

        public int Health => health;
        public EquipmentSettings[] Equipment => equipment;
    }
}