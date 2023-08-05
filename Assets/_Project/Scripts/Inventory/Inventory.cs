using System.Collections.Generic;
using System.Linq;

namespace HOT.Inventory
{
    public class Inventory
    {
        private const int FreeCellsInInventory = 10;
        
        private readonly InventoryCell[] cells;

        private Dictionary<InventoryCell, Item.Item> itemsInCellsMap = new Dictionary<InventoryCell, Item.Item>();

        public InventoryCell[] Cells => cells;
        
        public Inventory()
        {
            cells = new InventoryCell[FreeCellsInInventory];

            for (int i = 0; i < cells.Length; i++)
                cells[i] = new InventoryCell();
        }

        public bool AddItem(Item.Item item)
        {
            InventoryCell emptyCell = cells.FirstOrDefault(cell => !cell.IsFilled);

            if (emptyCell == null) return false;
            
            itemsInCellsMap.Add(emptyCell, item);
            emptyCell.AddItem(item);
            
            return true;
        }
    }
}