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
        
        [SerializeField] private ItemsDatabaseSettings itemsSettings;
        [SerializeField] private AssetReference cellAsset;
        [SerializeField] private Transform cellParent;

        private InventoryItemApplyer inventoryItemApplyer;
        private Dictionary<InventoryCellView, InventoryCell> cellsMap;

        private void Awake()
        {
            inventoryItemApplyer = new InventoryItemApplyer();
        }

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

            return cellView;
        }

        private void OnCellSelected(InventoryCellView cellView)
        {
            InventoryCell cell = cellsMap[cellView];
            
            if (cell.Item == null) return;
            
            inventoryItemApplyer.ApplyItem(cell.Item.Type, cell);
        }

        private Sprite GetItemIcon(InventoryCell cell) => cell.IsFilled ? GetItemSettings(cell.ItemId).Icon : null;
        private ItemSettings GetItemSettings(int itemId) => itemsDatabase.GetItem(itemId);
        
        private async Task<T> LoadAsset<T>(AssetReference asset) => await AddressableAssetLoader.LoadAsset<T>(asset);
    }
}