using ReLost.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReLost.Inventory.Items
{
    public class InventoryUISetter : MonoBehaviour
    {
        [SerializeField] private GameObject displayInventorySortedGameObject;
        [SerializeField] private DisplayInventorySorted displayInventorySorted;

        private void Update()
        {
            //UpdateSlotUI();
            if(displayInventorySorted.dynamicInterface.sorterToggle.isOn == true)
            {
                displayInventorySortedGameObject.SetActive(true);
            }
            else
            {
                displayInventorySortedGameObject.SetActive(false);
                //displayInventorySystem.SetInventorySlotIndex(); 
                NormalInventoryUI();
            }
        }

        //public void UpdateSlotUI()
        //{
        //    for (int i = 0; i < displayInventorySystem.playerInventory.container.itemSlots.Length; i++)
        //    {
        //        displayInventorySystem.PooledInventoryButton[i].GetComponent<InventorySlotUI>().UpdateSlotUI();
        //    }
        //}

        public void NormalInventoryUI()
        {
            for (int i = 0; i < displayInventorySorted.dynamicInterface.inventory.GetSlots.Length; i++)
            {
                displayInventorySorted.dynamicInterface.inventory.GetSlots[i].slotDisplay.transform.SetSiblingIndex(i);
            }
        }
    }
}
