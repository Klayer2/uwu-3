using ReLost.NPCs.Occupations.Vendors;
using ReLost.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ReLost.Inventory.Tooltip;

public class ReferenceHolder : MonoBehaviour
{
    public GameObject sellOrBuyQuantityConfirm;
    public GameObject vendorCanvas;
    public TMP_InputField sellOrBuyQuantityText;
    public TMP_InputField quantityToDestroy;
    public ItemDestroyer itemDestroyer;
    public BuySellQuantity buySellQuantity;
    public InventoryObject playerInventory;
    public VendorSystem vendorSystem;
    public PickUpDynamicInterface pickUpDynamicInterface;
    public HoverPopUpInfo hoverPopUpInfo;
    //private void Awake()
    //{
    //    playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    //}
}
