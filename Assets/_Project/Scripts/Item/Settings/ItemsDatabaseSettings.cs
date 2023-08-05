using System.Collections.Generic;
using UnityEngine;

namespace HOT.Inventory.Item
{
    [CreateAssetMenu(fileName = "ItemsDatabaseSettings", menuName = "HOT/Settings/Item/Database")]
    public class ItemsDatabaseSettings : ScriptableObject
    {
        [SerializeField] private List<ItemSettings> items;

        public List<ItemSettings> Items => items;
    }
}