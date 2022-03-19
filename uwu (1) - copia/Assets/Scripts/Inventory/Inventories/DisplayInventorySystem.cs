using ReLost.Inventory.Items;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace ReLost.Inventory
{
    public class DisplayInventorySystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> pooledInventoryButton;
        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        [SerializeField] private GameObject buttonPrefab = null;
        [SerializeField] private Transform buttonHolderTransform = null;
        public InventoryObject playerInventory;
        [SerializeField] private GameObject inventoryCanvas = null;
        [SerializeField] private DisplayInventorySorted displayInventorySorted;

        public List<GameObject> PooledInventoryButton => pooledInventoryButton;



        private void Awake()
        {
            pooledInventoryButton = new List<GameObject>();
            inventoryCanvas.SetActive(false);
        }
        private void Start()
        {
            //PoolInventorySlot();
        }

        //public void PoolInventorySlot()
        //{
        //    for (int i = 0; i < playerInventory.container.itemSlots.Length; i++)
        //    {
        //        GameObject buttonInstance = Instantiate(buttonPrefab, buttonHolderTransform);
        //        pooledInventoryButton.Add(buttonInstance);
        //        pooledInventoryButton[i].GetComponent<InventorySlotUI>().InventorySlotInitialise(playerInventory, i, displayInventorySorted);
        //        pooledInventoryButton[i].SetActive(true);
        //    }
        //}

        //public void SetInventorySlotIndex()
        //{
        //    for (int i = 0; i < playerInventory.container.itemSlots.Length; i++)
        //    {
        //        playerInventory.container.itemSlots[i].index = i;
        //        playerInventory.container.itemSlots[i].parent = this;
        //    }
        //}

        public void AddItemSlotGameObject()
        {

        }
    }
    public static class ExtensionMethods
    {
        public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
            {
                if (_slot.Value.item.Id >= 0)
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.uiDisplay;
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(1).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}


