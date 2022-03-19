using System;

namespace ReLost.Inventory.Items
{
    [Serializable]
    public struct ItemSlot : IComparable<ItemSlot>
    {
        public ItemObject item;
        public int quantity;
        public int id;

        public ItemSlot(ItemObject item, int quantity, int id)
        {
            this.item = item;
            this.quantity = quantity;
            this.id = id;
        }

        public int CompareTo(ItemSlot obj)
        {
            if(obj.item == null) { return 0; }
            if (this.item == null) { return 0; }
            return this.item.Name.CompareTo(obj.item.Name);
        }
    }

}
