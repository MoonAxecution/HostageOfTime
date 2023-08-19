using HOT.Equipment;
using UnityEngine;

namespace HOT.Player
{
    public class EquipmentView : MonoBehaviour
    {
        [Header("Equipment Points")]
        [SerializeField] private Transform weaponParent;
        [SerializeField] private Transform helmetParent;
        
        [Header("Equipment Prefabs")]
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private GameObject helmetPrefab;

        private IEquipmentCellReadOnly weaponEquipmentCell;
        private IEquipmentCellReadOnly helmetEquipmentCell;

        private GameObject equipedWeapon;
        private GameObject equipedHelmet;

        public void Init(IEquipmentCellReadOnly weaponEquipmentCell, IEquipmentCellReadOnly helmetEquipmentCell)
        {
            this.weaponEquipmentCell = weaponEquipmentCell;
            this.weaponEquipmentCell.Equiped += EquipWeapon;
            this.weaponEquipmentCell.TookOff += TakeOffWeapon;
            
            this.helmetEquipmentCell = helmetEquipmentCell;
            this.helmetEquipmentCell.Equiped += EquipHelmet;
            this.helmetEquipmentCell.TookOff += TakeOffHelmet;
            
            if (this.weaponEquipmentCell.IsFilled)
                EquipWeapon();
            
            if (this.helmetEquipmentCell.IsFilled)
                EquipHelmet();
        }
        
        private void EquipWeapon()
        {
            equipedWeapon = Instantiate(weaponPrefab, weaponParent);
        }

        private void EquipHelmet()
        {
            equipedHelmet = Instantiate(helmetPrefab, helmetParent);
        }

        private void TakeOffWeapon()
        {
            Destroy(equipedWeapon);
        }
        
        private void TakeOffHelmet()
        {
            Destroy(equipedHelmet);
        }

        private void OnDestroy()
        {
            this.weaponEquipmentCell.Equiped -= EquipWeapon;
            this.weaponEquipmentCell.TookOff -= TakeOffWeapon;

            this.helmetEquipmentCell.Equiped -= EquipHelmet;
            this.helmetEquipmentCell.TookOff -= TakeOffHelmet;
        }
    }
}
