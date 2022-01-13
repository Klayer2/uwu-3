using ReLost.Interactables;
using UnityEngine;

namespace ReLost.PlayerInventory.Items
{

    public class ItemPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemSlot[] itemSlot = new ItemSlot[9];
        [SerializeField] private int pickupMoney = 0;
        private bool isItemAvailable = true;
        public void Interact(GameObject other)
        {
            var itemContainer = other.GetComponent<Inventory>();

            itemContainer.AddMoney(pickupMoney);

            if(itemContainer == null) { return; }

            for(int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].item != null) { isItemAvailable = true; }
                if (itemSlot[i].item == null && isItemAvailable) { isItemAvailable = false; continue; }
                itemContainer.AddItem(itemSlot[i]);
            }
            if(isItemAvailable == false && pickupMoney == 0) { this.gameObject.SetActive(false); }

        }
    }

}

