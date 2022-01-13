using System;
using System.Linq;

namespace ReLost.PlayerInventory.Items
{
    [Serializable]
    public struct ItemSlot : IComparable<ItemSlot>
    {
        public InventoryItem item;
        public int quantity;

        public ItemSlot(InventoryItem item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        public int CompareTo(ItemSlot obj)
        {
            if(obj.item == null) { return 0; }
            if (this.item == null) { return 0; }
            return this.item.Name.CompareTo(obj.item.Name);
        }
    }

}
