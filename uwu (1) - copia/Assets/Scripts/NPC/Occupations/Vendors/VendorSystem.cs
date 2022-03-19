using ReLost.Inventory.Items;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace ReLost.NPCs.Occupations.Vendors
{
    public class VendorSystem : MonoBehaviour
    {
        private List<GameObject> pooledVendorButton;
        private int amountToPool = 1;
        [SerializeField] private GameObject buttonPrefab = null;
        [SerializeField] private Transform buttonHolderTransform = null;
        [SerializeField] private RarityList rarityListReference;
        [SerializeField] private TMP_InputField VendorItemSearchText = null;
        private VendorData scenarioData = null;
        private List<Item> itemsSorted;
        private int itemTypeEnumCount = ItemType.GetNames(typeof(ItemType)).Length;
        private int itemType = 0;
        private bool isEveryItemEmpty;
        public VendorData ScenarioData => scenarioData;

        private void Start()
        {
            itemsSorted = new List<Item>();
            pooledVendorButton = new List<GameObject>();
        }

        private void StartScenario(VendorData scenarioData)
        {
            this.scenarioData = scenarioData;

            SetCurrentContainer(true);
        }

        public void SetCurrentContainer(bool isBuying)
        {
            isEveryItemEmpty = true;
            ClearItemButtons();
            scenarioData.IsFirstContainerBuying = isBuying;
            var items = scenarioData.SellingItemContainer.GetAllUniqueItems();

            //for (int i = 0; i < rarityListReference.rarityList.Length; i++)
            //{
            //    for (int j = 0; j < items.Count; j++)
            //    {
            //        if (items[j] == null)
            //        {
            //            continue;
            //        }
            //        if (items[j].Rarity.Name == rarityListReference.rarityList[i].Name)
            //        {
            //            itemsSorted.Add(items[j]);
            //        }
            //    }
            //}
            for(int i = 0; i < items.Count; i++)
            {
                if (!isEveryItemEmpty) { continue; }
                if (items[i] == null) { continue; }
                if (items[i].Id >= 0) { isEveryItemEmpty = false; }
            }

            if (!isEveryItemEmpty)
            {
                for (int i = 0; i < rarityListReference.rarityList.Length; i++)
                {
                    for (int m = 0; m < itemTypeEnumCount; m++)
                    {
                        for (int j = 0; j < items.Count; j++)
                        {
                            if (items[j] == null || items[j].rarity == null)
                            {
                                continue;
                            }
                            if (items[j].rarity.name == rarityListReference.rarityList[i].name && items[j].itemTypeID == m)
                            {
                                itemsSorted.Add(items[j]);
                            }
                        }
                    }
                }

                do
                {
                    if (amountToPool > itemsSorted.Count)
                    {
                        break;
                    }
                    if (amountToPool <= itemsSorted.Count)
                    {
                        amountToPool++;
                    }
                    GameObject buttonInstance = Instantiate(buttonPrefab, buttonHolderTransform);
                    pooledVendorButton.Add(buttonInstance);
                } while (amountToPool <= itemsSorted.Count);

                for (int i = 0; i < itemsSorted.Count; i++)
                {
                    if(itemsSorted[i] == null) { continue; }
                    pooledVendorButton[i].GetComponent<VendorItemButton>().Initialise(this, itemsSorted[i], scenarioData.SellingItemContainer.GetTotalQuantity(itemsSorted[i]), scenarioData);
                    pooledVendorButton[i].SetActive(true);
                }
            }
                
            //for (int i = 0; i < itemsSorted.Length; i++)
            //{
            //    if (itemsSorted[i].item != null)
            //    {
            //        itemSlots[i] = itemSlotsSortedByRarity[i];
            //        itemSlotsSortedByRarity[i].item = null;
            //    }
            //}

            
        }

        private void ClearItemButtons()
        {
            foreach(Transform child in buttonHolderTransform)
            {
                child.gameObject.SetActive(false);
            }
            itemsSorted.Clear();
        }

        public void SearchVendorItem()
        {
            var vendorItemSearchText = VendorItemSearchText.text.ToLower();
            if (VendorItemSearchText == null) { return; }
            for (int i = 0; i < itemsSorted.Count; i++)
            {
                if (VendorItemSearchText.text == "") { pooledVendorButton[i].gameObject.SetActive(true); continue; }
                if (itemsSorted[i] == null) { continue; }

                if (itemsSorted[i].Name.ToLower().Contains(vendorItemSearchText))
                {
                    pooledVendorButton[i].gameObject.SetActive(true);
                }
                else
                if (itemsSorted[i].rarity.name.ToLower() == vendorItemSearchText)
                {
                    pooledVendorButton[i].gameObject.SetActive(true);
                }
                else
                {
                    pooledVendorButton[i].gameObject.SetActive(false);
                }
            }
        }

        public void SortByItemType(int type)
        {
            for (int i = 0; i < itemsSorted.Count; i++)
            {
                if (itemType == 100) { pooledVendorButton[i].gameObject.SetActive(true); continue; }
                if (itemsSorted[i] == null) { continue; }

                if (itemsSorted[i].itemTypeID == itemType)
                {
                    pooledVendorButton[i].gameObject.SetActive(true);
                    continue;
                }

                pooledVendorButton[i].gameObject.SetActive(false);

            }
        }

        public void SortByItemSubType(int subType)
        {
            

        }
    }
}


