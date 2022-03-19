using ReLost.NPCs.Occupations.Vendors;
using ReLost.PlayerNameSpace;
using ReLost.Inventory.Items;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BuySellQuantity : MonoBehaviour
{
    [SerializeField] private InventorySlot thisItem;
    [SerializeField] private TMP_InputField quantityToBuyOrSell = null;
    [SerializeField] private TextMeshProUGUI confirmationVendorTitle = null;
    [SerializeField] private TextMeshProUGUI confirmationVendorText = null;
    [SerializeField] private VendorSystem vendorSystem;
    [SerializeField] private GameObject confirmationVendorGameObject = null;
    public bool isPlayerInventorySelling = false;
    [SerializeField] private Player playerInventoryHolder;
    private VendorData scenarioData;
    private InventoryItemDataBase dataBase;
    private int inputQuantityToStringToInt;
    private int maxBuyableAmount;

    private void Awake()
    {
#if UNITY_EDITOR
        dataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif
        playerInventoryHolder = FindObjectOfType<Player>();
    }

    public void ItemReference(InventorySlot itemSlot, VendorData vendorData, bool isPlayerInventory)
    {
        thisItem = itemSlot;
        scenarioData = vendorData;
        isPlayerInventorySelling = isPlayerInventory;
        maxBuyableAmount = playerInventoryHolder.inventory.MaximumAmountOfItemThatCanGetIntoInventory(thisItem);
    }

    public void InventoryItemReference(InventorySlot itemSlot, bool isPlayerInventory)
    {   
        thisItem = itemSlot;
        isPlayerInventorySelling = isPlayerInventory;
        maxBuyableAmount = playerInventoryHolder.inventory.MaximumAmountOfItemThatCanGetIntoInventory(thisItem);

    }

    public void TextSBMaxSetter()
    {
        if (thisItem.item is Item inventoryItem)
        {
            if (isPlayerInventorySelling)
            {
                if (playerInventoryHolder.inventory.HasItem(inventoryItem))
                {
                    if (quantityToBuyOrSell.text != "")
                    {
                        int quantityCount = playerInventoryHolder.inventory.GetTotalQuantity(inventoryItem);
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
                    if (!inventoryItem.canBeBoughtInfinitely)
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
                        int maxBuyableQuantity = playerInventoryHolder.inventory.container.money / dataBase.ItemObjects[inventoryItem.Id].buyPrice;
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

    private void ConfirmBuyOrSell(InventorySlot itemToBuyOrSell)
    {

        if (isPlayerInventorySelling || !(scenarioData.IsFirstContainerBuying))
        {
            confirmationVendorTitle.text = "Vender"; 
            if (dataBase.ItemObjects[itemToBuyOrSell.item.Id].maxStack > 1)
            {
                if (itemToBuyOrSell.amount > 1)
                {
                    itemToBuyOrSell.amount = inputQuantityToStringToInt;
                }
                confirmationVendorText.text = $"¿Estas seguro de que quieres Vender: {itemToBuyOrSell.amount}x {itemToBuyOrSell.item.ColouredName} a { dataBase.ItemObjects[itemToBuyOrSell.item.Id].sellPrice * itemToBuyOrSell.amount} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
            else
            {
                confirmationVendorText.text = $"¿Estas seguro de que quieres Vender: {itemToBuyOrSell.item.ColouredName} a { dataBase.ItemObjects[itemToBuyOrSell.item.Id].sellPrice} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
        }

        else
        {
            confirmationVendorTitle.text = "Comprar";
            if (dataBase.ItemObjects[itemToBuyOrSell.item.Id].maxStack > 1)
            {
                if (itemToBuyOrSell.amount > 1)
                {
                    itemToBuyOrSell.amount = inputQuantityToStringToInt;
                }
                confirmationVendorText.text = $"¿Estas seguro de que quieres comprar: {itemToBuyOrSell.amount}x {itemToBuyOrSell.item.ColouredName} a { dataBase.ItemObjects[itemToBuyOrSell.item.Id].buyPrice * itemToBuyOrSell.amount} <sprite=0>?";
                confirmationVendorGameObject.SetActive(true);
            }
            else
            {
                confirmationVendorText.text = $"¿Estas seguro de que quieres comprar: {itemToBuyOrSell.item.ColouredName} a { dataBase.ItemObjects[itemToBuyOrSell.item.Id].buyPrice} <sprite=0>?";
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
                thisItem.amount = inputQuantityToStringToInt;
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
            playerInventoryHolder.AddMoney(dataBase.ItemObjects[thisItem.item.Id].sellPrice * inputQuantityToStringToInt);
            playerInventoryHolder.inventory.RemoveItem(thisItem.item, inputQuantityToStringToInt);
            vendorSystem.SetCurrentContainer(scenarioData.IsFirstContainerBuying);
            
        }
        else
        {
            if(thisItem.amount > maxBuyableAmount)
            {
                thisItem.amount = maxBuyableAmount;
            }
            playerInventoryHolder.ReduceMoney(dataBase.ItemObjects[thisItem.item.Id].buyPrice * thisItem.amount);
            playerInventoryHolder.inventory.AddItem(thisItem.item, thisItem.amount);
            if (!thisItem.item.canBeBoughtInfinitely)
            {
                scenarioData.SellingItemContainer.RemoveItem(thisItem.item, inputQuantityToStringToInt);
            }
            vendorSystem.SetCurrentContainer(scenarioData.IsFirstContainerBuying);
        }

    }
}
