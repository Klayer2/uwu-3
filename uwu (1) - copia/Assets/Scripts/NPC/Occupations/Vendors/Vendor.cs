using ReLost.Events;
using ReLost.PlayerNameSpace;
using ReLost.Inventory.Items;
using UnityEngine;

namespace ReLost.NPCs.Occupations.Vendors
{

    public class Vendor : MonoBehaviour, IOccupation
    {
        [SerializeField] private VendorDataEvent onStartVendorScenario = null;

        public string Name => "Name";

        [SerializeField] private InventoryHolder itemContainer = null;

        private void Start() => itemContainer = GetComponent<InventoryHolder>();

        public void Trigger(GameObject other)
        {
            var otherItemContainer = other.GetComponent<Player>();

            if(otherItemContainer == null)
            {
                return;
            }

            VendorData vendorData = new VendorData(otherItemContainer.inventory, itemContainer.Inventory);

            onStartVendorScenario.Raise(vendorData);

                
            
        }
    }

}


