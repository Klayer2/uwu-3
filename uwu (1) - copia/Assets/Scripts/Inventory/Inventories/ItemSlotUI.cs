using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ReLost.Inventory.Items
{

    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image itemIconImage = null;
        [SerializeField] protected Image itemIconBelowImage = null;

        public int SlotIndex { get; set; }

        public abstract Item SlotItem { get; set; }

        private void OnEnable() => UpdateSlotUI();

        protected virtual void Start()
        {
            UpdateSlotUI();
        }

        public abstract void OnDrop(PointerEventData eventData);

        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable)
        {
            itemIconImage.enabled = enable;
            itemIconBelowImage.enabled = enable;
        }
    }


}