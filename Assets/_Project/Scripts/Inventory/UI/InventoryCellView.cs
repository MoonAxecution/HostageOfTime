using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HOT.Inventory.UI
{
    public class InventoryCellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image icon;

        public event Action<InventoryCellView> Selected;
        public event Action<InventoryCellView> Updated;

        public InventoryCell Cell { get; private set; }

        public void Init(InventoryCell cell, Sprite icon)
        {
            Cell = cell;
            Cell.ItemAdded += OnCellUpdated;
            Cell.ItemRemoved += OnCellUpdated;

            UpdateIcon(icon);
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
            Cell.ItemAdded -= OnCellUpdated;
            Cell.ItemRemoved -= OnCellUpdated;
        }
    }
}