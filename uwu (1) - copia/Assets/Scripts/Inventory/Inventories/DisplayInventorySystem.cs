using ReLost.PlayerInventory.Items;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.PlayerInventory
{
    public class DisplayInventorySystem : MonoBehaviour
    {
        private List<GameObject> pooledInventoryButton;
        [SerializeField] private GameObject buttonPrefab = null;
        [SerializeField] private Transform buttonHolderTransform = null;
        [SerializeField] private Inventory playerInventory;
        [SerializeField] private GameObject inventoryCanvas = null;

        public List<GameObject> PooledInventoryButton => pooledInventoryButton;



        private void Awake()
        {
            inventoryCanvas.SetActive(false);
            pooledInventoryButton = new List<GameObject>();
        }
        private void Start()
        {
            PoolInventorySlot();
        }

        public void PoolInventorySlot()
        {
            for (int i = 0; i < playerInventory.ItemSlots.Length; i++)
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, buttonHolderTransform);
                pooledInventoryButton.Add(buttonInstance);
                pooledInventoryButton[i].GetComponent<InventorySlot>().InventorySlotInitialise(playerInventory);
                pooledInventoryButton[i].SetActive(true);
            }
        }

    }
}


