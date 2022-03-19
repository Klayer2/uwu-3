using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using ReLost.Inventory.Tooltip;
using System.Linq;

namespace ReLost.Inventory.Items
{
    public abstract class UserInterface : MonoBehaviour
    {

        public InventoryObject inventory;
        public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        public List<GameObject> objectsOnInterface;
        public HoverPopUpInfo hoverPopUpInfo;
        public GridLayoutGroup gridLayoutGroup;
        public GameObject tempItem;
        public GameObject sorterObject;
        public Toggle sorterToggle = null;

        private void OnEnable()
        {
            //for (int i = 0; i < inventory.GetSlots.Length; i++)
            //{
            //    inventory.GetSlots[i].parent = this;
            //    inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;

            //}

            if (sorterToggle != null)
            {
                if (sorterToggle.isOn == true)
                {
                    sorterObject.SetActive(true);
                }
            }
        }

        private void OnDisable()
        {
            if (sorterToggle != null)
            {
                sorterObject.SetActive(false);
            }
        }
        void Awake()
        {
            objectsOnInterface = new List<GameObject>();
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                inventory.GetSlots[i].parent = this;
                inventory.GetSlots[i].OnAfterUpdate += OnSlotUpdate;

            }
            CreateSlots();
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

            hoverPopUpInfo = FindObjectOfType<HoverPopUpInfo>();
        }
        public void SetInventorySlotIndex()
        {
            for (int i = 0; i < inventory.container.itemSlots.Length; i++)
            {
                inventory.container.itemSlots[i].index = i;
                inventory.container.itemSlots[i].parent = this;
            }
        }
        private void OnSlotUpdate(InventorySlot _slot)
        {
            if (_slot.item.Id >= 0)
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.uiDisplay;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? "" : _slot.amount.ToString("n0");
            }
            else
            {
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        // Update is called once per frame
        //void Update()
        //{
        //    slotsOnInterface.UpdateSlotDisplay();
        //}
        public void ByRaritySorter()
        {
            InventorySlot[] inventorySlotsSorted = new InventorySlot[inventory.container.itemSlots.Length];

            inventorySlotsSorted = inventory.container.itemSlots.OrderBy(itemSlots => itemSlots).ToArray();
            int freePlacement = 0;
            int iterations = 0;
            for (int i = 0; i < inventory.rarityListReference.rarityList.Length; i++)
            {
                iterations++;
                for (int m = 0; m < inventory.itemTypeEnumCount; m++)
                {
                    iterations++;
                    for (int j = 0; j < inventorySlotsSorted.Length; j++)
                    {
                        iterations++;
                        if (inventorySlotsSorted[j].item.Id < 0 || inventorySlotsSorted[j].item.rarity == null || inventorySlotsSorted[j].item.ItemTypeID != m)
                        {

                            continue;
                        }

                        if (inventorySlotsSorted[j].item.rarity.name == inventory.rarityListReference.rarityList[i].name)
                        {
                            inventorySlotsSorted[j].slotDisplay.transform.SetSiblingIndex(freePlacement);
                            freePlacement++;
                        }
                    }
                }
            }
            Debug.Log(1/Time.deltaTime);
            Debug.Log("inventory" + iterations);
        }
        public abstract void CreateSlots();

        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoveredOver = obj;
            hoverPopUpInfo.DisplayInfo(slotsOnInterface[obj].item);
        }
        public void OnExit(GameObject obj)
        {
            MouseData.slotHoveredOver = null;
            hoverPopUpInfo.HideInfo();
        }
        public void OnEnterInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
        }
        public void OnExitInterface(GameObject obj)
        {
            MouseData.interfaceMouseIsOver = null;
        }
        public void OnDragStart(GameObject obj)
        {
            if(sorterToggle.isOn == true)
            {
                return;
            }
            if (slotsOnInterface[obj].item.Id >= 0)
            {
                obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0.65f, 0.65f, 0.65f, 1f);
                MouseData.tempItemBeingDragged = CreateTempItem(obj);
            }
        }
        public GameObject CreateTempItem(GameObject obj)
        {
            if (slotsOnInterface[obj].item.Id >= 0)
            {
                var rt = tempItem.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
                var img = tempItem.GetComponent<Image>();
                img.sprite = slotsOnInterface[obj].ItemObject.uiDisplay;
                img.raycastTarget = false;
                tempItem.SetActive(true);
            }
            return tempItem;
        }
        public void OnDragEnd(GameObject obj)
        {
            if (sorterToggle.isOn == true)
            {
                return;
            }
            if (slotsOnInterface[obj].item.Id >= 0)
            {
                MouseData.tempItemBeingDragged.SetActive(false);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
                if (MouseData.interfaceMouseIsOver == null)
                {
                    slotsOnInterface[obj].RemoveItem();
                    return;
                }
                if (MouseData.slotHoveredOver)
                {
                    InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnInterface[MouseData.slotHoveredOver];
                    inventory.SwapItems(slotsOnInterface[obj], mouseHoverSlotData);
                }
            }
        }
        public void OnDrag(GameObject obj)
        {
            if (sorterToggle.isOn == true)
            {
                return;
            }
            if (slotsOnInterface[obj].item.Id >= 0)
            {
                if (MouseData.tempItemBeingDragged != null && MouseData.tempItemBeingDragged.activeInHierarchy == true)
                    MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }


    }
    public static class MouseData
    {
        public static UserInterface interfaceMouseIsOver;
        public static PickUpUserInterface pickUpInterfaceMouseIsOver;
        public static GameObject tempItemBeingDragged;
        public static GameObject slotHoveredOver;
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
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}

