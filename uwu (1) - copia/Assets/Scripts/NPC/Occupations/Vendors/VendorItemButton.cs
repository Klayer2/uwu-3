using ReLost.Events;
using ReLost.Inventory.Items;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ReLost.NPCs.Occupations.Vendors
{
    public class VendorItemButton : VendorItemUI, IPointerDownHandler
    {
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private TextMeshProUGUI inventoryItemQuantityText = null;
        [SerializeField] private TextMeshProUGUI thisItemQuantityText = null;
        [SerializeField] private TextMeshProUGUI thisItemPriceText = null;
        //[SerializeField] private Image itemIconImage = null;
        [SerializeField] private Item thisItem;
        [SerializeField] protected ItemEvent onStartHoveringItem = null;
        [SerializeField] private TMP_InputField quantityToBuyOrSell = null;
        [SerializeField] private GameObject confirmWindow = null;
        [SerializeField] private static ReferenceHolder referencer;
        private BuySellQuantity buySellQuantity = null;
        [SerializeField] private InventorySlot thisItemToSellOrBuy;
        [SerializeField] private VendorData scenarioData = null;
        [SerializeField] private InventoryItemDataBase dataBase;

        private void Awake()
        {
#if UNITY_EDITOR
            dataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif
        }

        public void Initialise(VendorSystem vendorSystem, Item item, int quantity, VendorData vendorData)
        {
            thisItemToSellOrBuy.item = item;
            thisItemToSellOrBuy.amount = quantity;
            scenarioData = vendorData;
            thisItem = item;
            thisItemQuantityText.text = quantity.ToString();
            ReferencesAsigner();
            itemNameText.text = $"{item.ColouredName} ";
            inventoryItemQuantityText.text = $"Tienes : <color=#FFD437>{referencer.playerInventory.GetTotalQuantity(item)}</color>";
            UpdateSlotUI();
            if (scenarioData.IsFirstContainerBuying)
            {
                if (item.canBeBoughtInfinitely) 
                { 
                    thisItemQuantityText.text = "∞"; 
                } 
                else
                {
                    thisItemQuantityText.text = $"{quantity}";
                }
                thisItemPriceText.text = $"{dataBase.ItemObjects[item.Id].buyPrice} <sprite=0>";
            }
            else
            {
                thisItemQuantityText.text = "";
                thisItemPriceText.text = $"{ dataBase.ItemObjects[item.Id].sellPrice} <sprite=0>";
            }
        }
        public override Item SlotItem
        {
            get
            { return thisItem; }
            set { thisItem = value; UpdateSlotUI(); }
        }

        public override void UpdateSlotUI()
        {
            if (thisItem.Id >= 0)
            {
                itemIconImage.sprite = dataBase.ItemObjects[thisItem.Id].UiDisplay;
                EnableSlotUI(true);
                return;

            }

            itemIconImage.sprite = null;


            EnableSlotUI(true);

            //SetItemQuantityUI();
        }

        protected override void EnableSlotUI(bool enable)
        {
            itemIconImage.enabled = enable;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                BuyOrSellCaller();
            }
        }

        private void ReferencesAsigner()
        {
            referencer = FindObjectOfType<ReferenceHolder>();
            confirmWindow = referencer.sellOrBuyQuantityConfirm;
            quantityToBuyOrSell = referencer.sellOrBuyQuantityText;
            buySellQuantity = referencer.buySellQuantity;
        }

        public void BuyOrSellCaller()
        {
            if (referencer.playerInventory.container.money >= dataBase.ItemObjects[thisItemToSellOrBuy.item.Id].buyPrice || !(scenarioData.IsFirstContainerBuying))
            {
                if (dataBase.ItemObjects[thisItemToSellOrBuy.item.Id].maxStack > 1)
                {
                    quantityToBuyOrSell.gameObject.SetActive(false);
                    quantityToBuyOrSell.gameObject.transform.position = Input.mousePosition;
                    buySellQuantity.ItemReference(thisItemToSellOrBuy, scenarioData, false);
                    quantityToBuyOrSell.text = "1";
                    quantityToBuyOrSell.gameObject.SetActive(true);
                }
            }
        }
    }
}


