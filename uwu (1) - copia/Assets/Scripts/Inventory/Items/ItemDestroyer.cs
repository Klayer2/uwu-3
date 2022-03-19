using TMPro;
using UnityEditor;
using UnityEngine;

namespace ReLost.Inventory.Items {
    public class ItemDestroyer : MonoBehaviour
    {
        private InventorySlot itemToDestroy;
        [SerializeField] private TMP_InputField inputQuantity;
        [SerializeField] private InventoryObject inventory = null;
        [SerializeField] private TextMeshProUGUI confirmationText = null;
        [SerializeField] private InventoryItemDataBase dataBase;
        private int inputQuantityToStringToInt;

        private void Awake()
        {
#if UNITY_EDITOR
            dataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif
        }

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
             var inventoryItem = itemToDestroy;
            if (inventoryItem != null)
            {

                if (inventory.HasItem(inventoryItem.item))
                {

                    int quantityCount = inventory.GetTotalQuantity(inventoryItem.item);
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

        public void SetItemSlot(InventorySlot itemSlot)
        {
            itemToDestroy = itemSlot;
        }
        public void ConfirmDestroy()
        {
            if (dataBase.ItemObjects[itemToDestroy.item.Id].maxStack > 1)
            {
                confirmationText.text = $"¿Estas seguro de que quieres destruir: {inputQuantityToStringToInt}x {itemToDestroy.item.ColouredName}?";
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
            if(dataBase.ItemObjects[itemToDestroy.item.Id].maxStack == 1)
            {
                inventory.RemoveAt(itemToDestroy.index);
            }
            else
            {
                inventory.RemoveItem(itemToDestroy.item, inputQuantityToStringToInt);
            }
            
            inputQuantityToStringToInt = 0;
        }
    }
}
