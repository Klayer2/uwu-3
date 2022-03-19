using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor;

namespace ReLost.Inventory.Items
{


//    public class InventoryItemDragHandler : ItemDragHandler
//    {
//        [SerializeField] private TMP_InputField quantityToDestroy;
//        [SerializeField] private ItemDestroyer itemDestroyer = null;
//        private InventorySlotUI actualItemSlot = null;
//        [SerializeField] private TMP_InputField quantityToBuyOrSell = null;
//        [SerializeField] private ReferenceHolder referencer;
//        [SerializeField] private GameObject vendorCanvas;
//        [SerializeField] private BuySellQuantity buySellQuantity = null;
//        [SerializeField] private InventoryItemDataBase dataBase;
//        private void Awake()
//        {
//#if UNITY_EDITOR
//            dataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
//#else
//            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
//#endif
//            referencer = FindObjectOfType<ReferenceHolder>();
//            itemDestroyer = referencer.itemDestroyer;
//            vendorCanvas = referencer.vendorCanvas;
//            quantityToDestroy = referencer.quantityToDestroy;
//            quantityToBuyOrSell = referencer.sellOrBuyQuantityText;
//            buySellQuantity = referencer.buySellQuantity;
//        }


//        public override void OnPointerUp(PointerEventData eventData)
//        {
//            if(eventData.button == PointerEventData.InputButton.Left)
//            {

//                base.OnPointerUp(eventData);

//                if(eventData.hovered.Count == 0)
//                {

//                    InventorySlotUI thisSlot = ItemSlotUI as InventorySlotUI;
//                    itemDestroyer.SetItemSlot(thisSlot.ItemSlot);
//                    actualItemSlot = thisSlot;
//                    if (dataBase.ItemObjects[thisSlot.ItemSlot.item.Id].maxStack > 1)
//                    {
//                        quantityToDestroy.gameObject.SetActive(false);
//                        quantityToDestroy.text = "1";
//                        quantityToDestroy.gameObject.SetActive(true);
//                    }
//                    else
//                    {
//                        itemDestroyer.ConfirmDestroy();
//                    }

//                }

//            }
//            if (eventData.button == PointerEventData.InputButton.Right)
//            {
//                base.OnPointerUp(eventData);

//                if (eventData.hovered.Count == 0)
//                {
//                    InventorySlotUI thisSlot = ItemSlotUI as InventorySlotUI;
//                    actualItemSlot = thisSlot;
//                    if (vendorCanvas.activeInHierarchy)
//                    {
//                        quantityToBuyOrSell.gameObject.SetActive(false);
//                        quantityToBuyOrSell.gameObject.transform.position = Input.mousePosition;
//                        buySellQuantity.InventoryItemReference(actualItemSlot.ItemSlot, true);
//                        quantityToBuyOrSell.text = "1";
//                        quantityToBuyOrSell.gameObject.SetActive(true);
//                    }
//                }
//            }
//        }

//    }
}