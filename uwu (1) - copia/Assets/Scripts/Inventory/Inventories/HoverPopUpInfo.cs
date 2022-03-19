using ReLost.Inventory.Items;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Inventory.Tooltip
{
    public class HoverPopUpInfo : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvasObject, itemBuffsHolder;
        [SerializeField] private RectTransform popupObject;
        [SerializeField] private TextMeshProUGUI itemName, itemBuffs, itemDescription, onUse;
        public TextMeshProUGUI itemSellPrice;
        [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
        [SerializeField] private float padding = 25f;
        [SerializeField] private Image itemIcon = null;
        [SerializeField] private InventoryItemDataBase inventoryItemDataBase;

        private Canvas popupCanvas = null;

        private void Awake()
        {
#if UNITY_EDITOR
            inventoryItemDataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            inventoryItemDataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif
            popupCanvas = popupCanvasObject.GetComponent<Canvas>();
        }

        private void OnDisable()
        {
            itemBuffsHolder.SetActive(false);
            onUse.gameObject.SetActive(false);
        }

        private void Update()
        {
            FollowCursor();
        }

        private void FollowCursor()
        {
            if (!popupCanvasObject.activeSelf) { return; }

            //calcular la posicion deseada
            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;


            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 64) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }

            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 0.9825f) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }

            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor / 32) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }

            float bottomEdgeToScreenEdgeDistance = 0 - (newPos.y - popupObject.rect.height * popupCanvas.scaleFactor / 0.9825f) + padding;
            if (bottomEdgeToScreenEdgeDistance > 0)
            {
                newPos.y += bottomEdgeToScreenEdgeDistance;
            }

            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(Item infoItem)
        {
            if (infoItem == null)
            {
                return;
            }
            if (infoItem.Id < 0)
            {
                return;
            }
            
            itemIcon.sprite = inventoryItemDataBase.ItemObjects[infoItem.Id].UiDisplay;
            StringBuilder builderName = new StringBuilder();
            StringBuilder builderBuffs = new StringBuilder();
            StringBuilder builderUse = new StringBuilder();
            StringBuilder builderDescription = new StringBuilder();
            StringBuilder builderPrice = new StringBuilder();

            builderName.Append(inventoryItemDataBase.ItemObjects[infoItem.Id].data.ColouredName).AppendLine();
            builderDescription.Append(inventoryItemDataBase.ItemObjects[infoItem.Id].description).AppendLine();
            builderUse.Append(inventoryItemDataBase.ItemObjects[infoItem.Id].useText).AppendLine(); 
            if (infoItem.buffs.Length > 0)
            {
                itemBuffsHolder.SetActive(true);
                for (int i = 0; i < infoItem.buffs.Length; i++)
                {
                    if(infoItem.buffs[i].value < 0)
                    {
                        builderBuffs.Append(infoItem.buffs[i].value + " " +infoItem.buffs[i].attribute).AppendLine(); 
                    }
                    else
                    if (infoItem.buffs[i].value > 0)
                    {
                        builderBuffs.Append("+" + infoItem.buffs[i].value + " " + infoItem.buffs[i].attribute).AppendLine();
                    }
                }
            }
            else
            {
                itemBuffsHolder.SetActive(false);
            }

            builderPrice.Append("Precio de Venta: " + inventoryItemDataBase.ItemObjects[infoItem.Id].sellPrice + " <sprite=0>");
            itemName.text = builderName.ToString();
            itemBuffs.text = builderBuffs.ToString();
            itemDescription.text = builderDescription.ToString();
            itemSellPrice.text = builderPrice.ToString();
            if (inventoryItemDataBase.ItemObjects[infoItem.Id].useText != "")
            {
                onUse.text = builderUse.ToString();
                onUse.gameObject.SetActive(true);
            }
            else
            {
                onUse.gameObject.SetActive(false);
            }

            popupCanvasObject.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }

        public void HideInfo()
        {
            popupCanvasObject.SetActive(false);
        }
    }
}


