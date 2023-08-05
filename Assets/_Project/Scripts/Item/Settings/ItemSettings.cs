using UnityEngine;

namespace HOT.Inventory.Item
{
    [CreateAssetMenu(fileName = "ItemSettings", menuName = "HOT/Settings/Item/Item")]
    public class ItemSettings : ScriptableObject
    {
        [SerializeField] private ItemType type;
        [SerializeField] private int id;
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;

        public ItemType Type => type;
        public int Id => id;
        public string ItemName => itemName;
        public Sprite Icon => icon;
    }
}