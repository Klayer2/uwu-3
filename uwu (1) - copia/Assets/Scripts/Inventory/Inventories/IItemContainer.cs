using System.Collections.Generic;
using TMPro;

namespace ReLost.Inventory.Items
{
    public interface IItemContainer
    {
        InventorySlot GetSlotByIndex(int index);
        int AddItem(Item _item, int _amount);
        List<Item> GetAllUniqueItems();
        void RemoveItem(Item _item, int _amount);
        //void RemoveAt(int slotIndex);
        void Swap(int indexOne, int indexTwo, TextMeshProUGUI inputTextField);
        bool HasItem(Item item);
        int GetTotalQuantity(Item item);
        void ByRaritySorter();
        void EmptyInventory();
    }
}
