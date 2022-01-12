using ReLost.PlayerInventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
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
        private List<InventoryItem> itemsSorted;

        public VendorData ScenarioData => scenarioData;

        private void Start()
        {
            itemsSorted = new List<InventoryItem>();
            pooledVendorButton = new List<GameObject>();
        }

        private void StartScenario(VendorData scenarioData)
        {
            this.scenarioData = scenarioData;

            SetCurrentContainer(true);
        }

        public void SetCurrentContainer(bool isBuying)
        {
            ClearItemButtons();
            scenarioData.IsFirstContainerBuying = isBuying;
            var items = scenarioData.SellingItemContainer.GetAllUniqueItems();

            for (int i = 0; i < rarityListReference.rarityList.Length; i++)
            {
                for (int j = 0; j < items.Count; j++)
                {
                    if (items[j] == null)
                    {
                        continue;
                    }
                    if (items[j].Rarity.Name == rarityListReference.rarityList[i].Name)
                    {
                        itemsSorted.Add(items[j]);                            
                    }
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
                pooledVendorButton[i].GetComponent<VendorItemButton>().Initialise(this, itemsSorted[i], scenarioData.SellingItemContainer.GetTotalQuantity(itemsSorted[i]), scenarioData);
                pooledVendorButton[i].SetActive(true);
            }
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
            if (VendorItemSearchText == null) { return; }
            for (int i = 0; i < itemsSorted.Count; i++)
            {
                if (VendorItemSearchText.text == "") { pooledVendorButton[i].gameObject.SetActive(true); continue; }
                if (itemsSorted[i] == null) { continue; }

                if (itemsSorted[i].name.ToLower().Contains(VendorItemSearchText.text.ToLower()))
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
                if (type == 0) { pooledVendorButton[i].gameObject.SetActive(true); continue; }
                if (itemsSorted[i] == null) { continue; }

                if (itemsSorted[i].ItemType == type)
                {
                    pooledVendorButton[i].gameObject.SetActive(true);
                }
                else
                {
                    pooledVendorButton[i].gameObject.SetActive(false);
                }
            }
        }
    }
}


