using ReLost.Events;
using ReLost.PlayerInventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.NPCs.Occupations.Vendors
{
    [RequireComponent(typeof(CanvasGroup))]
    public class VendorItemHoverHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {     
        
        [SerializeField] protected ItemSlotUI vendorItemUI = null;
        [SerializeField] protected ItemEvent onMouseStartHoverItem = null;
        [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

        private CanvasGroup canvasGroup = null;
        private Transform originalParent = null;
        private bool isHovering = false;

        public ItemSlotUI VendorItemUI => vendorItemUI;


        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnDisable()
        {
            if (isHovering)
            {
                onMouseEndHoverItem.Raise();
                isHovering = false;
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onMouseEndHoverItem.Raise();
                canvasGroup.blocksRaycasts = false;
            }
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                canvasGroup.blocksRaycasts = true;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onMouseStartHoverItem.Raise(vendorItemUI.SlotItem);
            isHovering = true;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            onMouseEndHoverItem.Raise();
            isHovering = false;
        }
    }
}




