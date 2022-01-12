using ReLost.PlayerInventory.Items;

namespace ReLost.NPCs.Occupations.Vendors
{
    public class VendorData
    {
        private IItemContainer buyingItemContainer = null;
        private IItemContainer sellingItemContainer = null;

        public VendorData
            (IItemContainer buyingItemContainer, 
            IItemContainer sellingItemContainer)
        {
            itemContainers[0] = buyingItemContainer;
            itemContainers[1] = sellingItemContainer;
        }

        private IItemContainer[] itemContainers = new IItemContainer[2];
        public bool IsFirstContainerBuying { get; set; } = true;
        public IItemContainer SellingItemContainer => IsFirstContainerBuying ? itemContainers[1] : itemContainers[0];
        public IItemContainer BuyingItemContainer => IsFirstContainerBuying ? itemContainers[0] : itemContainers[1];
        
    }
}

