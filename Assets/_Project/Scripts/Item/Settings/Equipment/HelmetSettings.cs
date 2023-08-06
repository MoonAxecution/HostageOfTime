using UnityEngine;

namespace HOT.Inventory.Item
{
    [CreateAssetMenu(fileName = "HelmetSettings", menuName = "HOT/Settings/Item/Helmet")]
    public class HelmetSettings : EquipmentSettings
    {
        [SerializeField] private int additionalHealth;

        public int AdditionalHealth => additionalHealth;
    }
}