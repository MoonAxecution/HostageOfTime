using System;
using HOT.Equipment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HOT.UI
{
    public class EquipmentCellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private EquipmentType equipmentType;
        [SerializeField] private Image icon;
        
        public event Action<EquipmentCellView> Selected;
        public event Action<EquipmentCellView> Updated;

        public EquipmentCell Cell { get; private set; }
        public EquipmentType EquipmentType => equipmentType;

        public void Init(EquipmentCell cell, Sprite iconSpite)
        {
            Cell = cell;
            Cell.Equiped += OnCellUpdated;
            Cell.TookOff += OnCellUpdated;
            
            UpdateIcon(iconSpite);
        }
        
        public void UpdateIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            icon.enabled = icon.sprite != null;
        }

        private void OnCellUpdated()
        {
            Updated.Fire(this);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Selected.Fire(this);
        }

        private void OnDestroy()
        {
            Cell.Equiped -= OnCellUpdated;
            Cell.TookOff -= OnCellUpdated;
        }
    }
}