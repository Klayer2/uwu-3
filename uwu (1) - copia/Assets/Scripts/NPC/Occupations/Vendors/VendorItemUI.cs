using ReLost.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ReLost.NPCs.Occupations.Vendors
{

    public abstract class VendorItemUI : MonoBehaviour
    {
        [SerializeField] protected Image itemIconImage = null;

        public int SlotIndex { get; private set; }

        public abstract Item SlotItem { get; set; }

        private void OnEnable() => UpdateSlotUI();

        protected virtual void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUI();
        }

        public abstract void UpdateSlotUI();

        protected virtual void EnableSlotUI(bool enable) => itemIconImage.enabled = enable;



    }

}

