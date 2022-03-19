using ReLost.Inventory.Items;

namespace ReLost.NPCs.Occupations.Vendors
{
    public class VendorData
    {
        private InventoryObject buyingItemContainer;
        private InventoryObject sellingItemContainer;

        public VendorData
            (InventoryObject buyingItemContainer,
            InventoryObject sellingItemContainer)
        {
            itemContainers[0] = buyingItemContainer;
            itemContainers[1] = sellingItemContainer;
        }

        private InventoryObject[] itemContainers = new InventoryObject[2];
        public bool IsFirstContainerBuying { get; set; } = true;
        public InventoryObject BuyingItemContainer => IsFirstContainerBuying ? itemContainers[0] : itemContainers[1];
        public InventoryObject SellingItemContainer => IsFirstContainerBuying ? itemContainers[1] : itemContainers[0];
        
        
    }
}

