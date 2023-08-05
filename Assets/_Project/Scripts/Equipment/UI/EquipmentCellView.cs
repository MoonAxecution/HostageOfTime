using System;
using HOT.Equipment;
using UnityEngine;
using UnityEngine.UI;

namespace HOT.UI
{
    public class EquipmentCellView : MonoBehaviour
    {
        [SerializeField] private EquipmentType equipmentType;
        [SerializeField] private Image icon;

        private EquipmentCell cell;

        public event Action<EquipmentCellView> CellUpdated; 

        public EquipmentType EquipmentType => equipmentType;

        public void Init(EquipmentCell cell, Sprite iconSpite)
        {
            this.cell = cell;
            cell.Equiped += OnEquiped;
            
            InitIcon(iconSpite);
        }
        
        public void InitIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            icon.enabled = icon.sprite != null;
        }

        private void OnEquiped()
        {
            CellUpdated.Fire(this);
        }

        private void OnDestroy()
        {
            cell.Equiped -= OnEquiped;
        }
    }
}