using ReLost.PlayerInventory.Items;

namespace ReLost.NPCs.Occupations.Vendors
{
    public class VendorData
    {
        private IItemContainer buyingItemContainer;
        private IItemContainer sellingItemContainer;

        public VendorData
            (IItemContainer buyingItemContainer,
            IItemContainer sellingItemContainer)
        {
            itemContainers[0] = buyingItemContainer;
            itemContainers[1] = sellingItemContainer;
        }

        private IItemContainer[] itemContainers = new IItemContainer[2];
        public bool IsFirstContainerBuying { get; set; } = true;
        public IItemContainer BuyingItemContainer => IsFirstContainerBuying ? itemContainers[0] : itemContainers[1];
        public IItemContainer SellingItemContainer => IsFirstContainerBuying ? itemContainers[1] : itemContainers[0];
        
        
    }
}

