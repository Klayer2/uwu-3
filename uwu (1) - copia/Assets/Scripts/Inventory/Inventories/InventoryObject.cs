using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using System;

public enum InterfaceType
{
    Inventory,
    Equipment,
    DropOrChest
}

namespace ReLost.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

    public class InventoryObject : ScriptableObject, IItemContainer, ISerializationCallbackReceiver
    {
        public string savePath;
        public RarityList rarityListReference;
        public InventoryItemDataBase inventoryItemDataBase;
        public int itemTypeEnumCount = Enum.GetNames(typeof(ItemType)).Length;
        public InterfaceType type;
        [SerializeField] private ItemObject itemToAddInEditor = null;
        [SerializeField] private int amountToAddInEditor = 0;
        [SerializeField] private int maxSlotsAmount = 0;
        public Inventory container;
        public InventorySlot[] GetSlots => container.itemSlots;

        
        private void Awake()
        {
#if UNITY_EDITOR
            inventoryItemDataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            inventoryItemDataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif
            for (int i = 0; i < container.itemSlots.Length; i++)
            {
                if (container.itemSlots[i].item.rarity == null)
                {
                    container.itemSlots[i].amount = 0;
                }
            }
        }

        public InventorySlot GetSlotByIndex(int index) => container.itemSlots[index];

        public int GetOcuppiedItemSlotsWithItem(InventorySlot itemSlot)
        {
            int occupiedItemSlotsWithItem = 0;
            for (int i = 0; i < container.itemSlots.Length; i++)
            {
                if(container.itemSlots[i].item.Id == itemSlot.item.Id)
                {
                    occupiedItemSlotsWithItem++;
                }
            }
            return occupiedItemSlotsWithItem;
        }

        public int GetSpaceRemainingInSlotOfItem(InventorySlot itemSlot)
        {
            int spaceRemainingInSlot = (GetOcuppiedItemSlotsWithItem(itemSlot) * inventoryItemDataBase.ItemObjects[itemSlot.item.Id].maxStack) - GetTotalQuantity(itemSlot.item);
            return spaceRemainingInSlot;
        }

        public InventorySlot[] AddSlots(InventorySlot[] inventorySlots, int slotsToBeAdded)
        {
            int _slotsToBeAdded = slotsToBeAdded;
            InventorySlot[] _inventorySlots = inventorySlots;

            if (_inventorySlots.Length + _slotsToBeAdded > maxSlotsAmount)
            {
                return _inventorySlots;
            }

            InventorySlot[] inventorySlots2 = new InventorySlot[_inventorySlots.Length + _slotsToBeAdded];

            for (int i = 0; i < _inventorySlots.Length; i++)
            {
                inventorySlots2[i] = _inventorySlots[i];
            }

            return inventorySlots2;
        }

        //public InventorySlot AddItem(InventorySlot itemSlot)
        //{
        //    if (itemSlot.item == null) { return new InventorySlot(); }
        //    if (GetAvailableSlots() == 0) { return itemSlot; }
        //    int thisItemInInventory = GetTotalQuantity(itemSlot.item);
        //    int spaceRemainingInSlot = (thisItemInInventory / itemSlot.item.maxStack) - thisItemInInventory;


        //    for (int i = 0; i < container.itemSlots.Length; i++)
        //    {
        //        if (container.itemSlots[i].item != null)
        //        {
        //            if (container.itemSlots[i].item.Id == itemSlot.item.Id)
        //            {
        //                int RemainingQuantityToBeAdded = container.itemSlots[i].item.maxStack - container.itemSlots[i].amount;

        //                if (itemSlot.amount <= RemainingQuantityToBeAdded)
        //                {
        //                    container.itemSlots[i].amount += itemSlot.amount;

        //                    itemSlot.amount = 0;

        //                    return itemSlot;
        //                }
        //                else if (RemainingQuantityToBeAdded > 0)
        //                {
        //                    container.itemSlots[i].amount += RemainingQuantityToBeAdded;

        //                    itemSlot.amount -= RemainingQuantityToBeAdded;

        //                }
        //            }
        //        }
        //    }

        //    for (int i = 0; i < container.itemSlots.Length; i++)
        //    {
        //        if (container.itemSlots[i].item.Id < 0)
        //        {
        //            if (itemSlot.amount <= itemSlot.item.maxStack)
        //            {
        //                container.itemSlots[i].item.Id = itemSlot.item.Id;
        //                container.itemSlots[i] = itemSlot;
        //                itemSlot.amount = 0;

        //                return itemSlot;
        //            }
        //            else
        //            {
        //                if (container.itemSlots[i].amount == itemSlot.item.maxStack) { continue; }
        //                container.itemSlots[i] = new InventorySlot(itemSlot.item, itemSlot.item.maxStack);

        //                itemSlot.amount -= itemSlot.item.maxStack;
        //            }
        //        }
        //    }

        //    return itemSlot;
        //}
        [ContextMenu("AddItem")]
        public void AddItemByMenu()
        {
            Item _item = new Item(itemToAddInEditor);
            AddItem(_item, amountToAddInEditor);
        }

        public int AddItem(Item _item, int _amount)
        {
            if(_amount == 0) { return _amount; }
            Item item = _item;
            int amount = _amount;
            int thisItemMaxStack = inventoryItemDataBase.ItemObjects[item.Id].maxStack;
            if (EmptySlotCount <= 0)
            {
                return amount;
            }
            InventorySlot slot = FindItemToAddOnInventory(item);
            if(amount > thisItemMaxStack)
            {
                int timesToAdd = /*(int)Math.Ceiling((double)*/(amount / thisItemMaxStack);
                for (int i = 0; i < timesToAdd; i++)
                {
                    if (EmptySlotCount <= 0)
                    {
                        return amount;
                    }
                    int RemainingQuantityToBeAddedInSlot;
                    slot = FindItemToAddOnInventory(item);
                    if(slot == null)
                    {
                        RemainingQuantityToBeAddedInSlot = thisItemMaxStack;
                    }
                    else
                    {
                        RemainingQuantityToBeAddedInSlot = thisItemMaxStack - slot.amount;
                    }
                    
                    
                    if (amount > thisItemMaxStack)
                    {
                        if (slot == null)
                        {
                            
                                SetEmptySlot(item, thisItemMaxStack);
                                amount -= thisItemMaxStack;
                                continue;
                        }
                        if(RemainingQuantityToBeAddedInSlot < amount)
                        {
                            slot.AddAmount(RemainingQuantityToBeAddedInSlot);
                            amount -= RemainingQuantityToBeAddedInSlot;
                        }
                        
                    }
                    if (slot == null && amount > 0 && amount <= thisItemMaxStack)
                    {
                        SetEmptySlot(item, amount);
                        amount = 0;
                        return amount;
                    }

                }
                if(amount == 0)
                {
                    return amount;
                }
                else
                {
                    return amount;
                }
            }
            if (slot == null)
            {
                SetEmptySlot(item, amount);
                amount = 0;
                return amount;
            }
            slot.AddAmount(amount);
            amount = 0;
            return amount;

            
        }

        public InventorySlot FindItemToAddOnInventory(Item _item)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if ((GetSlots[i].item.Id == _item.Id) && (GetSlots[i].amount < inventoryItemDataBase.ItemObjects[_item.Id].maxStack))
                {
                    return GetSlots[i];
                }
            }
            return null;
        }


        public int EmptySlotCount
        {
            get
            {
                int counter = 0;
                for (int i = 0; i < GetSlots.Length; i++)
                {
                    if (GetSlots[i].item.Id <= -1)
                    {
                        counter++;
                    }
                }
                return counter;
            }
        }

        public int GetTotalQuantity(Item _item)
        {
            if (_item == null) { return 0; }
            int totalCount = 0;
            

            for(int i = 0; i < container.itemSlots.Length; i++)
            {
                bool isTheSameItem = true;
                if (container.itemSlots[i].item.Id < 0) { continue; } // slot is empty so it doesnt try to get quantity
                if (container.itemSlots[i] == null) { continue; }
                if (container.itemSlots[i].item.Id != _item.Id) { continue; } // slot isnt the item required so it doesnt try to get quantity

                for(int j = 0; j < _item.buffs.Length; j++)
                {
                    if(_item.buffs[j].value != container.itemSlots[i].item.buffs[j].value)
                    {
                        isTheSameItem = false;
                        break;
                    }
                    
                }

                if(isTheSameItem == true)
                {
                    totalCount += container.itemSlots[i].amount;
                }
                
            }

            return totalCount;
        }

        public bool HasItem(Item item)
        {
            foreach (InventorySlot itemSlot in container.itemSlots)
            {
                if (itemSlot.item == null) { continue; }
                if (itemSlot.item != item) { continue; }
                return true;
            }
            return false;
        }

        public void RemoveAt(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex > container.itemSlots.Length - 1) { return; }

            container.itemSlots[slotIndex] = new InventorySlot();
        }

        public void RemoveItem(Item _item, int _amount)
        {
            if (_item == null) { return; }
            if (_item.isUsableInfinitely == true) { return; }

            int itemSlotMinus = _amount;
            for (int i = container.itemSlots.Length - 1; i > -1; i--)
            {
                if (itemSlotMinus > 0)
                {
                    if (container.itemSlots[i].item != null)
                    {

                        if (container.itemSlots[i].item.Id == _item.Id)
                        {
                            if (container.itemSlots[i].amount < itemSlotMinus)
                            {
                                itemSlotMinus -= container.itemSlots[i].amount;
                                container.itemSlots[i] = new InventorySlot();

                            }
                            else
                            {
                                container.itemSlots[i].amount -= itemSlotMinus;
                                itemSlotMinus -= _amount;

                                if (container.itemSlots[i].amount == 0)
                                {
                                    container.itemSlots[i] = new InventorySlot();

                                }
                            }
                        }

                    }
                }
            }

        }

        public List<Item> GetAllUniqueItems()
        {
            List<Item> items = new List<Item>();

            for (int i = 0; i < container.itemSlots.Length; i++)
            {
                if (container.itemSlots[i] == null || container.itemSlots[i].item == null || container.itemSlots[i].item.Id < 0) { continue; }
                if (items.Contains(container.itemSlots[i].item) == true) { continue; }
                if (items.Count == 0)
                {
                    if(container.itemSlots[i].item.Id >= 0)
                    {
                        items.Add(container.itemSlots[i].item);
                    }
                    
                    continue;
                }
                bool isTheSameItem = false;
                for (int j = 0; j < items.Count; j++)
                {
                    if(items[j] == null) { continue; }
                    if(items[j].Id != container.itemSlots[i].item.Id)
                    {
                        continue;
                    }
                    if (items[j].buffs.Length == 0 && container.itemSlots[i].item.buffs.Length == 0)
                    {
                        isTheSameItem = true;
                        break;
                    }
                    
                    for (int k = 0; k < items[j].buffs.Length; k++)
                    {
                        if (items[j].buffs[k].value != container.itemSlots[i].item.buffs[k].value)
                        {
                            isTheSameItem = false;
                            break;
                        }
                        else
                        {
                            isTheSameItem = true;
                            continue;
                        }
                        
                    }
                }
                if (isTheSameItem == false)
                {
                    items.Add(container.itemSlots[i].item);
                }
            }
            return items;
        }

        public void SwapItems(InventorySlot item1, InventorySlot item2)
        {
            if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
            {
                InventorySlot temp = new InventorySlot(item2.item, item2.amount);
                item2.UpdateSlot(item1.item, item1.amount);
                item1.UpdateSlot(temp.item, temp.amount);
            }
        }

        public void Swap(int indexOne, int indexTwo, TextMeshProUGUI inputTextField)
        {
            InventorySlot firstSlot = container.itemSlots[indexOne];
            InventorySlot secondSlot = container.itemSlots[indexTwo];

            if (firstSlot.Equals(secondSlot)) { return; }

            if (secondSlot.item != null)
            {
                if (firstSlot.item == secondSlot.item)
                {
                    int secondSlotRemainingSpace = inventoryItemDataBase.ItemObjects[secondSlot.item.Id].maxStack - secondSlot.amount;

                    if (firstSlot.amount <= secondSlotRemainingSpace)
                    {
                        container.itemSlots[indexTwo].amount += firstSlot.amount;

                        container.itemSlots[indexOne] = new InventorySlot();

                        return;
                    }
                }
            }
            container.itemSlots[indexOne] = secondSlot;
            container.itemSlots[indexTwo] = firstSlot;

        }

        public void EmptyInventory()
        {
            for (int i = 0; i < container.itemSlots.Length; i++)
            {
                container.itemSlots[i].item = null;
                container.itemSlots[i].amount = 0;
            }
        }

        public void ClearMoney()
        {
            container.money = 0;
        }

        [ContextMenu("Sorter")]
        public void ByRaritySorter()
        {

            var itemSlotsSortedByRarityAndType = new InventorySlot[container.itemSlots.Length];
            int freePlacement = 0;

            container.itemSlots = container.itemSlots.OrderBy(itemSlots => itemSlots).ToArray();


            for (int i = 0; i < rarityListReference.rarityList.Length; i++)
            {
                for(int m = 0; m < itemTypeEnumCount; m++)
                {
                    for (int j = 0; j < container.itemSlots.Length; j++)
                    {
                        if (container.itemSlots[j].item.Id < 0  || container.itemSlots[j].item.rarity == null || container.itemSlots[j].item.ItemTypeID != m)
                        {
                            continue;
                        }
                        if (container.itemSlots[j].item.rarity.name == rarityListReference.rarityList[i].name) 
                        {
                            itemSlotsSortedByRarityAndType[freePlacement] = container.itemSlots[j];
                            freePlacement++;
                        }
                    }                        
                }
            }

            //EmptyInventory();


            container.itemSlots = itemSlotsSortedByRarityAndType;
        }

        
        public int GetAvailableSlots()
        {

            int availableItemSlots = 0;

            for(int i = 0; i < container.itemSlots.Length; i++)
            {
                if(container.itemSlots[i].item.Id < 0)
                {
                    availableItemSlots++;
                }
            }
            return availableItemSlots;
        }

        public InventorySlot SetEmptySlot(Item _item, int _amount)
        {
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.Id < 0)
                {
                    GetSlots[i].UpdateSlot(_item, _amount);
                    return GetSlots[i];
                }
            }
            //set up functionality for full inventory
            return null;
        }

        //public InventorySlot FindItemOnInventory(Item _item)
        //{
        //    for (int i = 0; i < GetSlots.Length; i++)
        //    {
        //        if (GetSlots[i].item.Id == _item.Id)
        //        {
        //            return GetSlots[i];
        //        }
        //    }
        //    return null;
        //}

        public int MaximumAmountOfItemThatCanGetIntoInventory(InventorySlot itemSlot)
        {
            int maxAmount = (GetAvailableSlots() * inventoryItemDataBase.ItemObjects[itemSlot.item.Id].maxStack) + GetSpaceRemainingInSlotOfItem(itemSlot);
            return maxAmount;
        }

        [ContextMenu("Save")]
        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            binaryFormatter.Serialize(file, saveData);
            file.Close();
        }
        [ContextMenu("Load")]
        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
#if UNITY_EDITOR
                inventoryItemDataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
                rarityListReference = (RarityList)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/Rarity/RarityList.asset", typeof(RarityList));
#else
            inventoryItemDatabase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
            rarityListReference = Resources.Load<RarityList>("Items/Rarity/RarityList");
#endif
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(binaryFormatter.Deserialize(file).ToString(), this);
                file.Close();
            }
        }
        [ContextMenu("Clear")]
        public void Clear()
        {
            container = new Inventory();
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < container.itemSlots.Length; i++)
            {
                if (container.itemSlots[i].amount == 0)
                {
                    container.itemSlots[i].item.Id = -1;
                }
            }
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public InventorySlot[] itemSlots = new InventorySlot[0];
        public int money = 0;
    }

    public delegate void SlotUpdated(InventorySlot _slot);

    [System.Serializable]
    public class InventorySlot: IComparable<InventorySlot>
    {
         public ItemType[] AllowedItems = new ItemType[0];
        
        public UserInterface parent;
        public PickUpUserInterface pickUpParent;
        [System.NonSerialized]
        public GameObject slotDisplay;
        [System.NonSerialized]
        public SlotUpdated OnAfterUpdate;
        [System.NonSerialized]
        public SlotUpdated OnBeforeUpdate;
        public Item item = new Item();
        public int index;
        public int amount;

        public ItemObject ItemObject
        {
            get
            {
                
                if (item.Id >= 0)
                {
                    if (item.ColouredName == "") { return null; }
                    if(parent != null)
                    {
                        return parent.inventory.inventoryItemDataBase.ItemObjects[item.Id];
                    }
                    if(parent == null && pickUpParent != null)
                    {
                        return pickUpParent.inventory.inventoryItemDataBase.ItemObjects[item.Id];
                    }
                }
                return null;
            }
        }

        public InventorySlot()
        {
            UpdateSlot(new Item(), 0);
        }
        public InventorySlot(Item _item, int _amount)
        {
            UpdateSlot(_item, _amount);
        }
        public void UpdateSlot(Item _item, int _amount)
        {
            if (OnBeforeUpdate != null)
                OnBeforeUpdate.Invoke(this);
            item = _item;
            amount = _amount;
            if (OnAfterUpdate != null)
                OnAfterUpdate.Invoke(this);
        }
        public void RemoveItem()
        {
            UpdateSlot(new Item(), 0);
        }

        public void AddAmount(int value)
        {
            UpdateSlot(item, amount += value);
        }
        public bool CanPlaceInSlot(ItemObject _itemObject)
        {
            if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.Id < 0)
                return true;
            for (int i = 0; i < AllowedItems.Length; i++)
            {
                if (_itemObject.type == AllowedItems[i])
                    return true;
            }
            return false;
        }

        public int CompareTo(InventorySlot obj)
        {
            if (obj.item == null || obj.item.Name == "") { return 0; }
            if (this.item == null || obj.item.Name == "") { return 0; }
            return this.item.Name.CompareTo(obj.item.Name);
        }

    }
}