using TMPro;
using UnityEngine;

namespace ReLost.PlayerInventory.Items {
    public class ItemDestroyer : MonoBehaviour
    {
        private ItemSlot itemToDestroy;
        [SerializeField] private TMP_InputField inputQuantity;
        [SerializeField] private Inventory inventory = null;
        [SerializeField] private TextMeshProUGUI confirmationText = null;
        private int inputQuantityToStringToInt;

        public void GetQuantity()
        {
            if(inputQuantity.text != "")
            {
                inputQuantityToStringToInt = int.Parse(inputQuantity.text);

                if(inputQuantityToStringToInt > 0)
                {
                    ConfirmDestroy();
                }else 
                {
                    inputQuantity.gameObject.SetActive(false);
                    return; 
                }
            }else 
            {
                inputQuantity.gameObject.SetActive(false);
                return; 
            }
            
        }
        public void TextMaxSetter()
        {
             var inventoryItem = itemToDestroy.item;
            if (inventoryItem != null)
            {

                if (inventory.HasItem(inventoryItem))
                {

                    int quantityCount = inventory.GetTotalQuantity(inventoryItem);
                    if (inputQuantity.text != "")
                    {

                        if (int.Parse(inputQuantity.text) > quantityCount)
                        {

                            inputQuantity.text = quantityCount.ToString();
                            return;

                        }
                        if (int.Parse(inputQuantity.text) < 1f)
                        {

                            inputQuantity.text = "1";
                            return;

                        }

                        else { return; }

                    }

                }

            }

        }

        public void SetItemSlot(ItemSlot itemSlot)
        {
            this.itemToDestroy = itemSlot;
        }
        public void ConfirmDestroy()
        {
            if (itemToDestroy.item.MaxStack > 1)
            {
                itemToDestroy.quantity = inputQuantityToStringToInt;
                confirmationText.text = $"¿Estas seguro de que quieres destruir: {itemToDestroy.quantity}x {itemToDestroy.item.ColouredName}?";
                gameObject.SetActive(true);
            }else
            {
                confirmationText.text = $"¿Estas seguro de que quieres destruir: {itemToDestroy.item.ColouredName}?";
                gameObject.SetActive(true);
            }
            
            
        }

        public void Destroy()
        {

            inputQuantity.text = "";
            inventory.RemoveItem(itemToDestroy);
            inputQuantityToStringToInt = 0;
        }
    }
}
