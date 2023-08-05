using UnityEngine;

namespace HOT.Player
{
    public class EquipmentView : MonoBehaviour
    {
        [SerializeField] private Transform weaponParent;
        [SerializeField] private GameObject weaponPrefab;

        private GameObject equipedWeapon;
        
        public void EquipWeapon()
        {
            equipedWeapon = Instantiate(weaponPrefab, weaponParent);
        }

        public void TakeOffWeapon()
        {
            Destroy(equipedWeapon);
        }
    }
}
