using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using ReLost.Inventory.Tooltip;
using ReLost.PlayerNameSpace;
using ReLost.Interactables;
using System.Linq;

namespace ReLost.Inventory.Items
{
    public abstract class PickUpUserInterface : MonoBehaviour
    {
        public ItemPickup itemPickUp;
        public Dictionary<GameObject, InventorySlot> slotsOnPickUpInterface = new Dictionary<GameObject, InventorySlot>();
        public List<GameObject> gameObjects;
        public InventoryObject inventory;
        public HoverPopUpInfo hoverPopUpInfo;
        public GridLayoutGroup gridLayoutGroup;
        public GameObject tempItem;
        public ObjectInteractor objectInteractor;
        public TextMeshProUGUI moneyToPickUpText;

        private void OnEnable()
        {
            for (int i = 0; i < itemPickUp.itemSlotShowed.Length; i++)
            {
                itemPickUp.itemSlotShowed[i].slotDisplay = gameObjects[i];
                slotsOnPickUpInterface[gameObjects[i]] = itemPickUp.itemSlotShowed[i];
                itemPickUp.itemSlotShowed[i].pickUpParent = this;
                itemPickUp.itemSlotShowed[i].OnAfterUpdate += OnSlotUpdate;

            }
            if (itemPickUp == null) { return; }
            moneyToPickUpText.text = itemPickUp.pickupMoney.ToString();
            ByRaritySorter();
        }
        void Awake()
        {
            gameObjects = new List<GameObject>();
            inventory = FindObjectOfType<Player>().inventory;
            for (int i = 0; i < itemPickUp.itemSlotShowed.Length; i++)
            {
                itemPickUp.itemSlotShowed[i].pickUpParent = this;
                itemPickUp.itemSlotShowed[i].OnAfterUpdate += OnSlotUpdate;

            }
            CreateSlots();
            AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
            AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });

            hoverPopUpInfo = FindObjectOfType<HoverPopUpInfo>();
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

        public void Update()
        {
            for (int i = 0; i < itemPickUp.itemSlotShowed.Length; i++)
            {
                itemPickUp.itemSlotShowed[i].slotDisplay = gameObjects[i];
                slotsOnPickUpInterface[gameObjects[i]] = itemPickUp.itemSlotShowed[i];
                itemPickUp.itemSlotShowed[i].OnAfterUpdate += OnSlotUpdate;

            }
            moneyToPickUpText.text = itemPickUp.pickupMoney.ToString();
            slotsOnPickUpInterface.UpdatePickUpSlotDisplay();
        }
        public abstract void CreateSlots();

        public void ByRaritySorter()
        {
            InventorySlot[] inventorySlotsSorted = new InventorySlot[itemPickUp.itemSlotShowed.Length];

            inventorySlotsSorted = itemPickUp.itemSlotShowed.OrderBy(itemSlots => itemSlots).ToArray();
            int freePlacement = 0;
            int iterations = 0;
            for (int i = 0; i < inventory.rarityListReference.rarityList.Length; i++)
            {
                iterations++;
                for (int m = 0; m < inventory.itemTypeEnumCount; m++)
                {
                    iterations++;

                    foreach (KeyValuePair<GameObject, InventorySlot> _slot in slotsOnPickUpInterface)
                    {
                        iterations++;
                        if (_slot.Value.item.Id < 0 || _slot.Value.item == null || _slot.Value.item.rarity == null || _slot.Value.item.itemTypeID != m)
                        {
                            continue;
                        }

                        if(_slot.Value.item.rarity.name == inventory.rarityListReference.rarityList[i].name)
                        {
                            _slot.Key.transform.SetSiblingIndex(freePlacement);
                            freePlacement++;
                        }
                    }
                    
                }
            }

            Debug.Log("pick up" + iterations);
        }

        protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
        {
            EventTrigger trigger = obj.GetComponent<EventTrigger>();
            var eventTrigger = new EventTrigger.Entry();
            eventTrigger.eventID = type;
            eventTrigger.callback.AddListener(action);
            trigger.triggers.Add(eventTrigger);
        }

        public void OnMouseDown(GameObject obj)
        {
            
            hoverPopUpInfo.HideInfo();
        }

        public void OnMouseUpAsButton(GameObject obj)
        {
            if (Input.GetMouseButtonUp(1))
            {
                slotsOnPickUpInterface[obj].amount = inventory.AddItem(slotsOnPickUpInterface[obj].item, slotsOnPickUpInterface[obj].amount);

                if (slotsOnPickUpInterface[obj].amount == 0)
                {
                    slotsOnPickUpInterface[obj].item.Id = -1;
                    bool itemAvailable = false;
                    for (int i = 0; i < itemPickUp.itemSlotShowed.Length; i++)
                    {
                        if (itemPickUp.itemSlotShowed[i].amount > 0)
                        {
                            itemAvailable = true;
                        }
                    }

                    if (itemAvailable == false && itemPickUp.pickupMoney <= 0)
                    {
                        transform.parent.gameObject.SetActive(false);
                        itemPickUp.gameObject.SetActive(false);
                    }
                }
            }
            hoverPopUpInfo.DisplayInfo(slotsOnPickUpInterface[obj].item);
        }

        public void OnEnter(GameObject obj)
        {
            MouseData.slotHoveredOver = obj;
            hoverPopUpInfo.DisplayInfo(slotsOnPickUpInterface[obj].item);
        }
        public void OnExit(GameObject obj)
        {
            MouseData.slotHoveredOver = null;
            hoverPopUpInfo.HideInfo();
        }
        public void OnEnterInterface(GameObject obj)
        {
            MouseData.pickUpInterfaceMouseIsOver = obj.GetComponent<PickUpUserInterface>();
        }
        public void OnExitInterface(GameObject obj)
        {
            MouseData.pickUpInterfaceMouseIsOver = null;
            hoverPopUpInfo.HideInfo();
        }
        public void OnDragStart(GameObject obj)
        {
            hoverPopUpInfo.HideInfo();
            if (slotsOnPickUpInterface[obj].item.Id >= 0)
            {
                obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(0.65f, 0.65f, 0.65f, 1f);
                MouseData.tempItemBeingDragged = CreateTempItem(obj);
            }
        }
        public GameObject CreateTempItem(GameObject obj)
        {
            if (slotsOnPickUpInterface[obj].item.Id >= 0)
            {
                var rt = tempItem.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
                var img = tempItem.GetComponent<Image>();
                img.sprite = slotsOnPickUpInterface[obj].ItemObject.uiDisplay;
                img.raycastTarget = false;
                tempItem.SetActive(true);
            }
            return tempItem;
        }
        public void OnDragEnd(GameObject obj)
        {
            hoverPopUpInfo.HideInfo();
            if (slotsOnPickUpInterface[obj].item.Id >= 0)
            {
                tempItem.SetActive(false);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);
                if (MouseData.interfaceMouseIsOver == null)
                {
                    return;
                }
                if (MouseData.slotHoveredOver)
                {
                    if (slotsOnPickUpInterface.ContainsKey(obj))
                    {
                        slotsOnPickUpInterface[obj].amount = inventory.AddItem(slotsOnPickUpInterface[obj].item, slotsOnPickUpInterface[obj].amount);

                        if(slotsOnPickUpInterface[obj].amount == 0)
                        {
                            slotsOnPickUpInterface[obj].item = new Item();
                        }
                    }
                    
                }
            }
        }
        public void OnDrag(GameObject obj)
        {
            hoverPopUpInfo.HideInfo();
            if (slotsOnPickUpInterface[obj].item.Id >= 0)
            {
                if (MouseData.tempItemBeingDragged != null && MouseData.tempItemBeingDragged.activeInHierarchy == true)
                    MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }


    }

    public static class ExtensionPickUpMethods
    {
        public static void UpdatePickUpSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
        {
            foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
            {
                if (_slot.Value.item.Id >= 0 || _slot.Value.item == null)
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

