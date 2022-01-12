using System.Collections.Generic;
using TMPro;

namespace ReLost.PlayerInventory.Items
{
    public interface IItemContainer
    {
        int Money { get; set; }
        ItemSlot GetSlotByIndex(int index);
        ItemSlot AddItem(ItemSlot itemSlot);
        List<InventoryItem> GetAllUniqueItems();
        void RemoveItem(ItemSlot itemSlot);
        //void RemoveAt(int slotIndex);
        void Swap(int indexOne, int indexTwo, TextMeshProUGUI inputTextField);
        bool HasItem(InventoryItem item);
        int GetTotalQuantity(InventoryItem item);
        void ByRaritySorter();
        void ClearInventory();
    }
}
