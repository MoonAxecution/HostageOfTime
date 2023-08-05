using System;
using HOT.Inventory.Item;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HOT.Inventory.UI
{
    public class InventoryCellView : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image icon;

        public event Action<InventoryCellView> Selected;

        public InventoryCell Cell { get; private set; }

        public void Init(InventoryCell cell, Sprite icon)
        {
            Cell = cell;
            Cell.ItemRemoved += ClearCell;

            InitIcon(icon);
        }

        private void InitIcon(Sprite sprite)
        {
            icon.sprite = sprite;
            icon.enabled = icon.sprite != null;
        }

        private void ClearCell()
        {
            InitIcon(null);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Selected.Fire(this);
        }
    }
}