using ReLost.PlayerInventory.Items;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.PlayerInventory.Tooltip
{
    public class HoverPopUpInfo : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvasObject;
        [SerializeField] private RectTransform popupObject;
        [SerializeField] private TextMeshProUGUI itemName, itemDescription, onUse;
        [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);
        [SerializeField] private float padding = 25f;
        [SerializeField] private Image itemIcon = null;

        private Canvas popupCanvas = null;

        private void Awake()
        {
            popupCanvas = popupCanvasObject.GetComponent<Canvas>();
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
            itemIcon.sprite = infoItem.Icon;
            StringBuilder builderName = new StringBuilder();
            StringBuilder builderDescription = new StringBuilder();
            StringBuilder builderInfo = new StringBuilder();

            builderName.Append(infoItem.ColouredName).AppendLine(); 

            builderDescription.Append(infoItem.Description).AppendLine();
            builderInfo.Append(infoItem.GetInfoDisplayText());

            itemName.text = builderName.ToString();
            itemDescription.text = builderDescription.ToString();
            onUse.text = builderInfo.ToString();

            popupCanvasObject.SetActive(true);

            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }

        public void HideInfo()
        {
            popupCanvasObject.SetActive(false);
        }
    }
}


