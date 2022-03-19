using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.Inventory.Items
{


    //public class InventorySlotUI : ItemSlotUI, IDropHandler
    //{

    //    [SerializeField] private InventoryObject playerInventory = null;
    //    [SerializeField] private TextMeshProUGUI itemQuantityText = null;
    //    private InventorySlot pickUpSlot = null;
    //    private DisplayInventorySorted inventorySorted;
    //    public GameObject inventorySlotOutliner = null;
    //    public int realSlotIndex;

    //    public override Item SlotItem
    //    {

    //        get { return ItemSlot.item; }
    //        set { }

    //    }

    //    private void Awake()
    //    {
    //        SlotIndex = realSlotIndex;
    //    }

    //    public InventorySlot ItemSlot => playerInventory.GetSlotByIndex(SlotIndex);

    //    public override void OnDrop(PointerEventData eventData) 
    //    {
            
    //        ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

    //        if (pickUpSlot != null)
    //        {
    //            //playerInventory.SwapItems();
    //        }

    //        if (itemDragHandler == null) { return; }

    //        if((itemDragHandler.ItemSlotUI as InventorySlotUI) != null && (inventorySorted.sortedToogle.isOn == false))
    //        {
    //            playerInventory.Swap(itemDragHandler.ItemSlotUI.SlotIndex, SlotIndex, itemQuantityText);
    //        }
    //    }

    //    public void PickUpSlotInitialise(InventorySlot inventorySlot, int index)
    //    {
    //        realSlotIndex = index;
    //        UpdatePickUpSlotUI(inventorySlot);
    //    }

    //    public void InventorySlotInitialise(InventoryObject inventory, int index, DisplayInventorySorted displayInventorySorted)
    //    {
    //        realSlotIndex = index;
    //        playerInventory = inventory;
    //        inventorySorted = displayInventorySorted;
    //    }

    //    public override void UpdateSlotUI()
    //    {
    //        if(playerInventory == null) { return; }
    //        if(ItemSlot.item == null || ItemSlot.amount <= 0)
    //        {
    //            EnableSlotUI(false);
    //            return;
    //        }

    //        EnableSlotUI(true);

    //        itemIconImage.sprite = ItemSlot.item.uiDisplay;
    //        itemIconBelowImage.sprite = ItemSlot.item.uiDisplay;
    //        itemQuantityText.text = ItemSlot.amount > 1 ? ItemSlot.amount.ToString() : "";
    //    }

    //    public void UpdatePickUpSlotUI(InventorySlot pickUpSlot)
    //    {
    //        if (ItemSlot.item == null || ItemSlot.amount <= 0)
    //        {
    //            EnableSlotUI(false);
    //            return;
    //        }

    //        EnableSlotUI(true);

    //        itemIconImage.sprite = pickUpSlot.item.uiDisplay;
    //        itemIconBelowImage.sprite = pickUpSlot.item.uiDisplay;
    //        itemQuantityText.text = pickUpSlot.amount > 1 ? pickUpSlot.amount.ToString() : "";
    //    }

    //    protected override void EnableSlotUI(bool enable)
    //    {
    //        base.EnableSlotUI(enable);
    //        itemQuantityText.enabled = enable;
    //    }
    //}
}