using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.Inventory.Items.Hotbars
{
//    public class HotbarSlot : ItemSlotUI, IDropHandler
//    {
//        [SerializeField] private InventoryObject inventory = null;
//        [SerializeField] private TextMeshProUGUI itemQuantityText = null;
//        private InventoryItemDataBase dataBase;

//        private Item slotItem = null;

//        private void Awake()
//        {

//#if UNITY_EDITOR
//            dataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
//#else
//            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
//#endif
//        }

//        public override Item SlotItem 
//        { 
//            get { return slotItem; }
//            set { slotItem = value; UpdateSlotUI(); }
//        }

//        public bool AddItem(Item itemToAdd)
//        {
//            if(SlotItem != null) { return false; }

//            SlotItem = itemToAdd;

//            return true;
//        }

//        public void UseSlot(int index)
//        {
//            if (index != SlotIndex) { return; }

//            //use item
//        }

//        public override void OnDrop(PointerEventData eventData)
//        {
//            ItemDragHandler itemDragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

//            if(itemDragHandler == null) { return; }

//            InventorySlotUI inventorySlot = itemDragHandler.ItemSlotUI as InventorySlotUI;

//            if(inventorySlot != null)
//            {
//                SlotItem = inventorySlot.ItemSlot.item;
//            }

//            HotbarSlot hotbarSlot = itemDragHandler.ItemSlotUI as HotbarSlot;

//            if(hotbarSlot!= null)
//            {
//                Item oldItem = SlotItem;
//                SlotItem = hotbarSlot.SlotItem;
//                hotbarSlot.SlotItem = oldItem;
//                return;
//            }
//        }

//        public override void UpdateSlotUI()
//        {
//            if(SlotItem == null)
//            {
//                EnableSlotUI(false);
//                return;

//            }

//            itemIconImage.sprite = inventory.inventoryItemDataBase.ItemObjects[SlotItem.Id].UiDisplay;
//            itemIconBelowImage.sprite = inventory.inventoryItemDataBase.ItemObjects[SlotItem.Id].UiDisplay;

//            EnableSlotUI(true);

//            SetItemQuantityUI();
//        }

//        private void SetItemQuantityUI()
//        {
//            if(SlotItem is Item inventoryItem)
//            {
//                if (inventory.HasItem(inventoryItem))
//                {
//                    int quantityCount = inventory.GetTotalQuantity(inventoryItem);
//                    itemQuantityText.text = quantityCount > 1 || dataBase.ItemObjects[inventoryItem.Id].maxStack > 1 ? quantityCount.ToString() : "";
//                }
//                else
//                {
//                    SlotItem = null;
//                }
//            }
//            else
//            {
//                itemQuantityText.enabled = false;
//            }
//        }

//        protected override void EnableSlotUI(bool enable)
//        {
//            base.EnableSlotUI(enable);
//            itemQuantityText.enabled = enable;
//        }


//    }

}
