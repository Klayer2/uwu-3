using ReLost.Interactables;
using ReLost.PlayerNameSpace;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ReLost.Inventory.Items
{

    public class ItemPickup : MonoBehaviour, IInteractable, ISerializationCallbackReceiver
    {
        public ItemLootTable itemLootTable;
        public InventorySlot[] itemSlotShowed = new InventorySlot[8];
        public ReferenceHolder referenceHolder;
        public List<ObjectInteractor> objectInteractors;
        public PickUpDynamicInterface pickUpDynamicInterface;
        [SerializeField] private InventoryItemDataBase inventoryItemDataBase;
        public int pickupMoney = 0;
        private bool isItemAvailable = true;

        private void OnEnable()
        {
            objectInteractors = new List<ObjectInteractor>();
            referenceHolder = FindObjectOfType<ReferenceHolder>();
            pickUpDynamicInterface = referenceHolder.pickUpDynamicInterface;
#if UNITY_EDITOR
            inventoryItemDataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif
            for (int i = 0; i < itemLootTable.itemDrops.Length; i++)
            {
                pickupMoney = Random.Range(itemLootTable.minMoney, itemLootTable.maxMoney);
                float chance = 1 / itemLootTable.itemDrops[i].chanceOutOfOne;
                float dropped = Random.Range(0, chance);
                
                if (( dropped >= 0) && (dropped <= 1))
                {
                    itemSlotShowed[i].amount = Random.Range(1, itemLootTable.itemDrops[i].maxAmountToBeDropped);
                    Item _item = new Item(inventoryItemDataBase.ItemObjects[itemLootTable.itemDrops[i].itemObject.data.Id]);
                    itemSlotShowed[i].item = _item;
                    itemSlotShowed[i].index = i;
                }
            }
        }

        private void OnDisable()
        {
            RemoveFromEveryObjectInteractor(this);
        }

        public void Interact(GameObject other)
        {
            var itemContainer = other.GetComponent<Player>();
            objectInteractors.Add(other.GetComponentInChildren<ObjectInteractor>());
            pickUpDynamicInterface.itemPickUp = this;

            for(int i = 0; i < itemSlotShowed.Length; i++)
            {
                itemSlotShowed[i].pickUpParent = pickUpDynamicInterface;
            }
            if(pickUpDynamicInterface.transform.parent.gameObject.activeInHierarchy == true)
            {
                itemContainer.AddMoney(pickupMoney);

                pickupMoney = 0;

                if (itemContainer == null) { return; }



                for (int i = 0; i < itemSlotShowed.Length; i++)
                {
                    if (itemSlotShowed[i].item.rarity == null && isItemAvailable) { isItemAvailable = false; continue; }
                    if (itemSlotShowed[i].item.rarity != null) { isItemAvailable = true; }

                    itemSlotShowed[i].amount = itemContainer.inventory.AddItem(itemSlotShowed[i].item, itemSlotShowed[i].amount);

                    if (itemSlotShowed[i].amount == 0)
                    {
                        itemSlotShowed[i].item = null;
                    }

                }
                if (isItemAvailable == false && pickupMoney == 0) 
                { 
                    pickUpDynamicInterface.transform.parent.gameObject.SetActive(false);
                    pickUpDynamicInterface.itemPickUp = null;
                    this.gameObject.SetActive(false); 
                }
                
            }
            else
            {
                pickUpDynamicInterface.transform.parent.gameObject.SetActive(true);
            }
            
        }

        public void RemoveFromEveryObjectInteractor(ItemPickup itemPickup)
        {
            for (int i = 0; i < objectInteractors.Count; i++)
            {
                if(objectInteractors[i] == null) { objectInteractors.RemoveAt(i); }
                if (i >= objectInteractors.Count) { return; }
                objectInteractors[i].objectsToInteract.Remove(itemPickup);
                objectInteractors[i].objectsToInteractGameObject.Remove(itemPickup.transform);
            }
        }

        public void OnBeforeSerialize()
        {

        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < itemSlotShowed.Length; i++)
            {
                if (itemSlotShowed[i].amount == 0)
                {
                    itemSlotShowed[i].item.Id = -1;
                }
            }
        }


    }
}

