using ReLost.Events;
using ReLost.PlayerInventory.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.NPCs.Occupations.Vendors
{
    public class VendorItemButton : ItemSlotUI, IPointerDownHandler
    {
        [SerializeField] private TextMeshProUGUI itemNameText = null;
        [SerializeField] private TextMeshProUGUI inventoryItemQuantityText = null;
        [SerializeField] private TextMeshProUGUI thisItemQuantityText = null;
        [SerializeField] private TextMeshProUGUI thisItemPriceText = null;
        [SerializeField] private Item thisItem = null;
        [SerializeField] protected ItemEvent onStartHoveringItem = null;
        [SerializeField] private TMP_InputField quantityToBuyOrSell = null;
        [SerializeField] private GameObject confirmWindow = null;
        [SerializeField] private static ReferenceHolder referencer;
        private BuySellQuantity buySellQuantity = null;
        [SerializeField] private ItemSlot thisItemToSellOrBuy;
        [SerializeField] private VendorData scenarioData = null;


        public override Item SlotItem 
        {
            get 
            { return thisItem; }
            set { thisItem = value; UpdateSlotUI(); }
        }

        public void Initialise(VendorSystem vendorSystem, InventoryItem item, int quantity, VendorData vendorData)
        {
            thisItemToSellOrBuy.item = item;
            thisItemToSellOrBuy.quantity = quantity;
            scenarioData = vendorData;
            thisItem = item;
            thisItemQuantityText.text = quantity.ToString();
            ReferencesAsigner();
            itemNameText.text = $"{item.ColouredName} ";
            inventoryItemQuantityText.text = $"Tienes : <color=#FFD437>{referencer.playerInventory.GetTotalQuantity(item)}</color>";
            UpdateSlotUI();
            if (scenarioData.IsFirstContainerBuying)
            {
                if (item.IsUnlimited) 
                { 
                    thisItemQuantityText.text = "∞"; 
                } 
                else
                {
                    thisItemQuantityText.text = $"{quantity}";
                }
                thisItemPriceText.text = $"{item.BuyPrice} <sprite=0>";
            }
            else
            {
                thisItemQuantityText.text = "";
                thisItemPriceText.text = $"{item.SellPrice}";
            }
        }

        public override void UpdateSlotUI()
        {
            if (thisItem == null)
            {
                EnableSlotUI(false);
                return;

            }

            itemIconImage.sprite = thisItem.Icon;

            EnableSlotUI(true);

            //SetItemQuantityUI();
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                BuyOrSellCaller();
            }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            return;
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
            if (referencer.playerInventory.Money >= thisItemToSellOrBuy.item.BuyPrice || !(scenarioData.IsFirstContainerBuying))
            {
                if (thisItemToSellOrBuy.item.MaxStack > 1)
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


