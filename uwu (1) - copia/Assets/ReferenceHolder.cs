using ReLost.NPCs.Occupations.Vendors;
using ReLost.PlayerInventory.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public GameObject sellOrBuyQuantityConfirm;
    public GameObject vendorCanvas;
    public TMP_InputField sellOrBuyQuantityText;
    public TMP_InputField quantityToDestroy;
    public ItemDestroyer itemDestroyer;
    public BuySellQuantity buySellQuantity;
    public Inventory playerInventory;
    public VendorSystem vendorSystem;
    //private void Awake()
    //{
    //    playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    //}
}
