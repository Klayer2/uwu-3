using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.PlayerInventory.Items
{


    public class InventorySlot : ItemSlotUI, IDropHandler
    {

        [SerializeField] private Inventory playerInventory = null;
        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
        [SerializeField] public GameObject inventorySlotOutliner = null;


        public override Item SlotItem
        {

            get { return ItemSlot.item; }
            set { }

        }

        public ItemSlot ItemSlot => playerInventory.GetSlotByIndex(SlotIndex);

        public override void OnDrop(PointerEventData eventData) 
        {

            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            if(itemDragHandler == null) { return; }

            if((itemDragHandler.ItemSlotUI as InventorySlot) != null)
            {
                playerInventory.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex, itemQuantityText);
            }
        }

        public void InventorySlotInitialise(Inventory inventory)
        {
            playerInventory = inventory;
        }

        public override void UpdateSlotUI()
        {
            if(ItemSlot.item == null || ItemSlot.quantity <= 0)
            {
                EnableSlotUI(false);
                return;
            }

            EnableSlotUI(true);

            itemIconImage.sprite = ItemSlot.item.Icon;
            itemQuantityText.text = ItemSlot.quantity > 1 ? ItemSlot.quantity.ToString() : "";
        }

        protected override void EnableSlotUI(bool enable)
        {
            base.EnableSlotUI(enable);
            itemQuantityText.enabled = enable;
        }
    }
}