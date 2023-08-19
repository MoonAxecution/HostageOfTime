using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HOT.Addressable;
using HOT.Inventory.Item;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace HOT.Inventory.UI
{
    public class InventoryView : MonoBehaviour
    {
        [Inject] private ItemsDatabase itemsDatabase;
        
        [SerializeField] private AssetReference cellAsset;
        [SerializeField] private Transform cellParent;

        private Dictionary<InventoryCellView, InventoryCell> cellsMap;

        public event Action<InventoryCell> ItemSelected;

        public void Init(InventoryCell[] cells)
        {
            this.Inject();
            CreateCells(cells);
        }

        private async Task CreateCells(InventoryCell[] cells)
        {
            var loadedCellPrefab = await LoadAsset<InventoryCellView>(cellAsset);

            cellsMap = new Dictionary<InventoryCellView, InventoryCell>();

            foreach (var cell in cells)
                cellsMap.Add(CreateCell(loadedCellPrefab, cell), cell);
        }

        private InventoryCellView CreateCell(InventoryCellView cellPrefab, InventoryCell cell)
        {
            InventoryCellView cellView = Instantiate(cellPrefab, cellParent);
            cellView.Init(cell, GetItemIcon(cell));
            cellView.Selected += OnCellSelected;
            cellView.Updated += OnCellUpdated;

            return cellView;
        }

        private void OnCellSelected(InventoryCellView cellView)
        {
            InventoryCell cell = cellsMap[cellView];
            
            if (cell.Item == null) return;
            
            ItemSelected.Fire(cell);
        }

        private void OnCellUpdated(InventoryCellView cellView)
        {
            cellView.UpdateIcon(GetItemIcon(cellView.Cell));
        }

        private Sprite GetItemIcon(InventoryCell cell) => cell.IsFilled ? GetItemSettings(cell.ItemId).Icon : null;
        private ItemSettings GetItemSettings(int itemId) => itemsDatabase.GetItem(itemId);
        
        private async Task<T> LoadAsset<T>(AssetReference asset) => await AddressableAssetLoader.LoadAsset<T>(asset);
    }
}