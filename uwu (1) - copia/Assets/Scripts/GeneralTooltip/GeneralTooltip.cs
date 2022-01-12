using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ReLost.Tooltip
{
    public class GeneralTooltip : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI headerField = null;

        [SerializeField]private TextMeshProUGUI contentField = null;

        [SerializeField] private LayoutElement layoutElement = null;

        [SerializeField] private RectTransform rectTransform;

        [SerializeField] private Canvas canvas;

        [SerializeField] private Vector3 offset = new Vector3(0f, 50f, 0f);

        [SerializeField] private float padding = 25f;

        public void SetText(string content, string header = "")
        {
            if (string.IsNullOrEmpty(header))
            {
                headerField.gameObject.SetActive(false);
            }
            else
            {
                headerField.gameObject.SetActive(true);
                headerField.text = header;
            }
            contentField.text = content;
            layoutElement.enabled = Math.Max(headerField.preferredWidth, contentField.preferredWidth) > layoutElement.preferredWidth;
        }

        private void Update()
        {
            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;


            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + rectTransform.rect.width * canvas.scaleFactor) - padding +15f;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }

            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - rectTransform.rect.width * canvas.scaleFactor / 64) + padding - 30f;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }

            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + rectTransform.rect.height * canvas.scaleFactor / 32) - padding + 15f;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }

            float bottomEdgeToScreenEdgeDistance = 0 - (newPos.y - rectTransform.rect.height * canvas.scaleFactor) + padding - 15f;
            if (bottomEdgeToScreenEdgeDistance > 0)
            {
                newPos.y += bottomEdgeToScreenEdgeDistance;
            }

            rectTransform.transform.position = newPos;
        }

    }
}