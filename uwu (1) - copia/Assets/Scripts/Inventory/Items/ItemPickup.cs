using ReLost.Interactables;
using UnityEngine;

namespace ReLost.PlayerInventory.Items
{

    public class ItemPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemSlot[] itemSlot = new ItemSlot[9];
        private bool isItemAvailable = true;
        public void Interact(GameObject other)
        {
            var itemContainer = other.GetComponent<IItemContainer>();

            if(itemContainer == null) { return; }

            for(int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].item == null) { isItemAvailable = false; continue; }
                itemContainer.AddItem(itemSlot[i]);

                //if (itemContainer.AddItem(itemSlot[i]).quantity == 0) { isItemAvailable = false; };
            }
            if(itemSlot == null) { print("k"); }

            if (!isItemAvailable)
            {
                Destroy(gameObject);
            }
        }
    }

}

