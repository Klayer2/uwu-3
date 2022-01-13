using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace ReLost.PlayerInventory.Items
{

    public class Inventory : MonoBehaviour, IItemContainer
    {
        [SerializeField] private UnityEvent onInventoryItemsUpdated = null;
        [SerializeField] private ItemSlot[] itemSlots = new ItemSlot[0];
        [SerializeField] private int money = 0;
        [SerializeField] private RarityList rarityListReference;
        [SerializeField] private TMP_InputField itemFindText = null;
        [SerializeField] private TextMeshProUGUI moneyText = null;
        [SerializeField] private DisplayInventorySystem displayInventorySystem;
        [SerializeField] private ItemTypeList ItemTypeListReference;

        private void Awake()
        {
            displayInventorySystem = FindObjectOfType<DisplayInventorySystem>();

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    itemSlots[i].quantity = 0;
                }
            }
        }

        private void Start()
        {
            if (!this.gameObject.CompareTag("Player"))
            {
                ByRaritySorter();
            }
        }

        public void AddMoney(int moneyIn)
        {
            money += moneyIn;
            moneyText.text = $"{money} <sprite=0>";
        }

        public void ReduceMoney(int moneyOut)
        {
            money += moneyOut;
            moneyText.text = $"{money} <sprite=0>";
        }

        public void SetMoneyText()
        {
            moneyText.text = $"{money} <sprite=0>";
        }

        public ItemSlot[] ItemSlots => itemSlots;
        public UnityEvent OnInventoryItemsUpdated => onInventoryItemsUpdated;
        public int Money { get { return money; } set { money = value; } }

        public ItemSlot GetSlotByIndex(int index) => itemSlots[index];

        public int GetOcuppiedItemSlotsWithItem(ItemSlot itemSlot)
        {
            int thisItemTotalQuantity = GetTotalQuantity(itemSlot.item);
            int occupiedItemSlots = thisItemTotalQuantity / itemSlot.item.MaxStack + ((thisItemTotalQuantity % itemSlot.item.MaxStack == 0) ? 0 : 1);
            return occupiedItemSlots;
        }

        public int GetSpaceRemainingInSlotOfItem(ItemSlot itemSlot)
        {
            int spaceRemainingInSlot = (GetOcuppiedItemSlotsWithItem(itemSlot) * itemSlot.item.MaxStack) - GetTotalQuantity(itemSlot.item);
            return spaceRemainingInSlot;
        }

        public ItemSlot AddItem(ItemSlot itemSlot)
        {
            if(itemSlot.item == null) { return new ItemSlot(); }
            if (GetAvailableSlots() == 0) { return itemSlot; }
            int thisItemInInventory = GetTotalQuantity(itemSlot.item);
            int spaceRemainingInSlot = (thisItemInInventory / itemSlot.item.MaxStack) - thisItemInInventory;
            

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item != null)
                {
                    if (itemSlots[i].item == itemSlot.item)
                    {
                        int RemainingQuantityToBeAdded = itemSlots[i].item.MaxStack - itemSlots[i].quantity;

                        if (itemSlot.quantity <= RemainingQuantityToBeAdded)
                        {
                            itemSlots[i].quantity += itemSlot.quantity;

                            itemSlot.quantity = 0;

                            onInventoryItemsUpdated.Invoke();

                            return itemSlot;
                        }
                        else if (RemainingQuantityToBeAdded > 0)
                        {
                            itemSlots[i].quantity += RemainingQuantityToBeAdded;

                            itemSlot.quantity -= RemainingQuantityToBeAdded;

                        }
                    }
                }
            }

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null)
                {
                    if (itemSlot.quantity <= itemSlot.item.MaxStack)
                    {
                        itemSlots[i] = itemSlot;

                        itemSlot.quantity = 0;

                        onInventoryItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else
                    {
                        itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                        itemSlot.quantity -= itemSlot.item.MaxStack;
                    }
                }
            }

            onInventoryItemsUpdated.Invoke();

            return itemSlot;
        }

        public int GetTotalQuantity(InventoryItem item)
        {
            if (item == null) { return 0; }
            int totalCount = 0;

            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null) { continue; } // slot is empty so it doesnt try to get quantity
                if (itemSlot.item != item) { continue; } // slot isnt the item required so it doesnt try to get quantity

                totalCount += itemSlot.quantity;
            }

            return totalCount;
        }

        public bool HasItem(InventoryItem item)
        {
            foreach (ItemSlot itemSlot in itemSlots)
            {
                if (itemSlot.item == null) { continue; }
                if (itemSlot.item != item) { continue; }
                return true;
            }
            return false;
        }

        //public void RemoveAt(int slotIndex)
        //{
        //    if(slotIndex < 0 || slotIndex > itemSlots.Length - 1) { return; }

        //    itemSlots[slotIndex] = new ItemSlot();
        //    onInventoryItemsUpdated.Invoke();
        //}

        public void RemoveItem(ItemSlot itemSlot)
        {
            if (itemSlot.item == null) { return; }
            if (itemSlot.item.IsUsableInfinitely == true) { return; }

            int itemSlotMinus = itemSlot.quantity;
            for (int i = itemSlots.Length - 1; i > -1; i--)
            {
                if (itemSlotMinus > 0)
                {
                    if (itemSlots[i].item != null)
                    {

                        if (itemSlots[i].item == itemSlot.item)
                        {
                            if (itemSlots[i].quantity < itemSlotMinus)
                            {
                                itemSlotMinus -= itemSlots[i].quantity;
                                itemSlots[i] = new ItemSlot();

                            }
                            else
                            {
                                itemSlots[i].quantity -= itemSlotMinus;
                                itemSlotMinus -= itemSlot.quantity;

                                if (itemSlots[i].quantity == 0)
                                {
                                    itemSlots[i] = new ItemSlot();

                                }
                            }
                        }

                    }
                }
            }
            onInventoryItemsUpdated.Invoke();

        }

        public List<InventoryItem> GetAllUniqueItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null) { continue; }

                if (items.Contains(itemSlots[i].item)) { continue; }

                items.Add(itemSlots[i].item);
            }

            return items;
        }

        public void Swap(int indexOne, int indexTwo, TextMeshProUGUI inputTextField)
        {
            ItemSlot firstSlot = itemSlots[indexOne];
            ItemSlot secondSlot = itemSlots[indexTwo];

            if (firstSlot.Equals(secondSlot)) { return; }

            if (secondSlot.item != null)
            {
                if (firstSlot.item == secondSlot.item)
                {
                    int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                    if (firstSlot.quantity <= secondSlotRemainingSpace)
                    {
                        itemSlots[indexTwo].quantity += firstSlot.quantity;

                        itemSlots[indexOne] = new ItemSlot();

                        onInventoryItemsUpdated.Invoke();

                        return;
                    }
                }
            }
            itemSlots[indexOne] = secondSlot;
            itemSlots[indexTwo] = firstSlot;

            onInventoryItemsUpdated.Invoke();

        }

        public void ClearInventory()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].item = null;
            }
        }

        public void ByRaritySorter()
        {

            var itemSlotsSortedByRarityAndType = new ItemSlot[itemSlots.Length];
            var item = new List<ItemSlot>();

            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].item == null) { continue; }
                for (int j = 0; j < itemSlots.Length; j++)
                {
                    if (itemSlots[j].item == null) { continue; }
                    if (i == j) { continue; }
                    if (itemSlots[i].item == itemSlots[j].item)
                    {
                        this.AddItem(itemSlots[j]);
                        this.RemoveItem(itemSlots[j]);
                    }
                }
            }

            itemSlots = itemSlots.OrderBy(itemSlots => itemSlots).ToArray();


            for (int i = 0; i < rarityListReference.rarityList.Length; i++)
            {
                for(int m = 0; m < ItemTypeListReference.ItemType.Length; m++)
                {
                    for (int n = 0; n < ItemTypeListReference.ItemSubType.Length; n++)
                    {
                        for (int j = 0; j < itemSlots.Length; j++)
                        {
                            if (itemSlots[j].item == null)
                            {
                                continue;
                            }
                            if (itemSlots[j].item.Rarity.Name == rarityListReference.rarityList[i].Name && itemSlots[j].item.ItemType == m && itemSlots[j].item.ItemSubType == n)
                            {
                                for (int k = 0; k < itemSlots.Length; k++)
                                {
                                    if (itemSlotsSortedByRarityAndType[k].item == null)
                                    {
                                        itemSlotsSortedByRarityAndType[k] = itemSlots[j];
                                        break;
                                    }
                                }
                            }
                        }
                    }
                        
                }
            }

            ClearInventory();


            itemSlots = itemSlotsSortedByRarityAndType;

            OnInventoryItemsUpdated.Invoke();
        }

        
        public int GetAvailableSlots()
        {

            int availableItemSlots = 0;

            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemSlots[i].item == null)
                {
                    availableItemSlots++;
                }
            }
            return availableItemSlots;
        }

        public int MaximumAmountOfItemThatCanGetIntoInventory(ItemSlot itemSlot)
        {
            int maxAmount = (GetAvailableSlots() * itemSlot.item.MaxStack) + GetSpaceRemainingInSlotOfItem(itemSlot);
            return maxAmount;
        }

        public void SearchItem()
        {
            var ItemFindText = itemFindText.text.ToLower();
            if (itemFindText == null) { return; }
            for(int i = 0; i < itemSlots.Length; i++)
            {
                if(itemFindText.text == "") { displayInventorySystem.PooledInventoryButton[i].GetComponent<InventorySlot>().inventorySlotOutliner.gameObject.SetActive(false); continue; }
                if(itemSlots[i].item == null) { continue; }

                if (itemSlots[i].item.name.ToLower().Contains(ItemFindText))
                {
                    displayInventorySystem.PooledInventoryButton[i].GetComponent<InventorySlot>().inventorySlotOutliner.gameObject.SetActive(true);
                }
                else
                if(itemSlots[i].item.Rarity.Name.ToLower() == (ItemFindText))
                {
                    displayInventorySystem.PooledInventoryButton[i].GetComponent<InventorySlot>().inventorySlotOutliner.gameObject.SetActive(true);
                }
                else
                {
                    displayInventorySystem.PooledInventoryButton[i].GetComponent<InventorySlot>().inventorySlotOutliner.gameObject.SetActive(false);
                }
            }
        }
    }
}