using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBuyQuantityLocation : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Canvas canvas;

    [SerializeField] private float padding = 25f;
    private void OnEnable()
    {
        {
            Vector3 newPos = Input.mousePosition;
            newPos.z = 0f;


            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + rectTransform.rect.width * canvas.scaleFactor) - padding + 15f;
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
