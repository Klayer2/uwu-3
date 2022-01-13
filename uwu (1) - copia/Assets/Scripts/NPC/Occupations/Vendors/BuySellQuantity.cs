using ReLost.NPCs.Occupations.Vendors;
using ReLost.PlayerInventory.Items;
using TMPro;
using UnityEngine;

public class BuySellQuantity : MonoBehaviour
{
    [SerializeField] private ItemSlot thisItem;
    [SerializeField] private TMP_InputField quantityToBuyOrSell = null;
    [SerializeField] private TextMeshProUGUI confirmationVendorTitle = null;
    [SerializeField] private TextMeshProUGUI confirmationVendorText = null;
    [SerializeField] private VendorSystem vendorSystem;
    [SerializeField] private GameObject confirmationVendorGameObject = null;
    public bool isPlayerInventorySelling = false;
    [SerializeField] private Inventory playerInventory;
    private VendorData scenarioData;
    private int inputQuantityToStringToInt;
    private int maxBuyableAmount;



    public void ItemReference(ItemSlot itemSlot, VendorData vendorData, bool isPlayerInventory)
    {
        thisItem = itemSlot;
        scenarioData = vendorData;
        isPlayerInventorySelling = isPlayerInventory;
        maxBuyableAmount = playerInventory.MaximumAmountOfItemThatCanGetIntoInventory(thisItem);
    }

    public void InventoryItemReference(ItemSlot itemSlot, bool isPlayerInventory)
    {
        thisItem = itemSlot;
        isPlayerInventorySelling = isPlayerInventory;
        maxBuyableAmount = playerInventory.MaximumAmountOfItemThatCanGetIntoInventory(thisItem);
    }

    public void TextSBMaxSetter()
    {
        if (thisItem.item is InventoryItem inventoryItem)
        {
            if (isPlayerInventorySelling)
            {
                if (playerInventory.HasItem(inventoryItem))
                {
                    if (quantityToBuyOrSell.text != "")
                    {
                        int quantityCount = playerInventory.GetTotalQuantity(inventoryItem);
                        if (int.Parse(quantityToBuyOrSell.text) > quantityCount)
                        {
                            quantityToBuyOrSell.text = quantityCount.ToString();
                            return;
                        }
                        if (int.Parse(quantityToBuyOrSell.text) >= 1f) { return; }
                        else
                        {
                            quantityToBuyOrSell.text = "1";
                            return;
                        }

                    }

                }
            }

            if (!isPlayerInventorySelling && scenarioData.SellingItemContainer.HasItem(inventoryItem))
            {
                if (quantityToBuyOrSell.text != "")
                {
                    if (!inventoryItem.IsUnlimited)
                    {
                        int quantityCount = scenarioData.SellingItemContainer.GetTotalQuantity(inventoryItem);
                        if (int.Parse(quantityToBuyOrSell.text) > maxBuyableAmount)
                        {
                            quantityToBuyOrSell.text = maxBuyableAmount.ToString();
                            return;
                        }
                        if (int.Parse(quantityToBuyOrSell.text) > quantityCount)
                        {
                            quantityToBuyOrSell.text = quantityCount.ToString();
                            return;
                        }

                    }
                    else
                    {
                        int maxBuyableQuantity = playerInventory.Money / inventoryItem.BuyPrice;
                        if (int.Parse(quantityToBuyOrSell.text) > maxBuyableAmount)
                        {
                            quantityToBuyOrSell.text = maxBuyableAmount.ToString();
                            return;
                        }
                        if (int.Parse(quantityToBuyOrSell.text) > maxBuyableQuantity)
                        {
                            quantityToBuyOrSell.text = maxBuyableQuantity.ToString();
                            return;
                        }

                    }
                    
                    if (int.Parse(quantityToBuyOrSell.text) < 1f) 
                    {
                        quantityToBuyOrSell.text = "1";
                        return;
                    }
                }
            }
        }
    }

    private void ConfirmBuyOrSell(ItemSlot itemToBuyOrSell)
    {

        if (isPlayerInventorySelling || !(scenarioData.IsFirstContainerBuying))
        {
            confirmationVendorTitle.text = "Vender";
            if (itemToBuyOrSell.item.MaxStack > 1)
            {
                if (itemToBuyOrSell.quantity > 1)
                {
                    itemToBuyOrSell.quantity = inputQuantityToStringToInt;
                }
                confirmationVendorText.text = $"¿Estas seguro de que quieres Vender: {itemToBuyOrSell.quantity}x {itemToBuyOrSell.item.ColouredName} a {itemToBuyOrSell.item.SellPrice * itemToBuyOrSell.quantity} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
            else
            {
                confirmationVendorText.text = $"¿Estas seguro de que quieres Vender: {itemToBuyOrSell.item.ColouredName} a {itemToBuyOrSell.item.SellPrice} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
        }

        else
        {
            confirmationVendorTitle.text = "Comprar";
            if (itemToBuyOrSell.item.MaxStack > 1)
            {
                if (itemToBuyOrSell.quantity > 1)
                {
                    itemToBuyOrSell.quantity = inputQuantityToStringToInt;
                }
                confirmationVendorText.text = $"¿Estas seguro de que quieres comprar: {itemToBuyOrSell.quantity}x {itemToBuyOrSell.item.ColouredName} a {itemToBuyOrSell.item.BuyPrice * itemToBuyOrSell.quantity} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
            else
            {
                confirmationVendorText.text = $"¿Estas seguro de que quieres comprar: {itemToBuyOrSell.item.ColouredName} a {itemToBuyOrSell.item.BuyPrice} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
        }
    }

    public void GetVendorItemQuantity()
    {
        if (quantityToBuyOrSell.text != "")
        {
            inputQuantityToStringToInt = int.Parse(quantityToBuyOrSell.text);

            if (inputQuantityToStringToInt > 0)
            {
                thisItem.quantity = inputQuantityToStringToInt;
                ConfirmBuyOrSell(thisItem);
            }
            else
            {
                quantityToBuyOrSell.gameObject.SetActive(false);
                return;
            }
        }
        else
        {
            quantityToBuyOrSell.gameObject.SetActive(false);
            return;
        }

    }

    public void BuyOrSell()
    {
        if (isPlayerInventorySelling || !(scenarioData.IsFirstContainerBuying))
        { 
            playerInventory.AddMoney(thisItem.item.SellPrice * inputQuantityToStringToInt);
            playerInventory.RemoveItem(thisItem);
            vendorSystem.SetCurrentContainer(scenarioData.IsFirstContainerBuying);
            
        }
        else
        {
            if(thisItem.quantity > maxBuyableAmount)
            {
                thisItem.quantity = maxBuyableAmount;
            }
            playerInventory.ReduceMoney(thisItem.item.BuyPrice * thisItem.quantity);
            playerInventory.AddItem(thisItem);
            if (!thisItem.item.IsUnlimited)
            {
                scenarioData.SellingItemContainer.RemoveItem(thisItem);
            }
            vendorSystem.SetCurrentContainer(scenarioData.IsFirstContainerBuying);
        }

    }
}
