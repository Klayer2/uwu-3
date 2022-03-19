using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.Inventory.Items
{
    public class PickUpDynamicInterface : PickUpUserInterface
    {
        public GameObject buttonPrefab;
        [SerializeField] private Transform buttonHolderTransform = null;

        public override void CreateSlots()
        {
            slotsOnPickUpInterface = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < itemPickUp.itemSlotShowed.Length; i++)
            {
                GameObject buttonInstance = Instantiate(buttonPrefab, buttonHolderTransform);

                AddEvent(buttonInstance, EventTriggerType.PointerEnter, delegate { OnEnter(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.PointerExit, delegate { OnExit(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.Drag, delegate { OnDrag(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.PointerDown, delegate { OnMouseDown(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.PointerUp, delegate { OnMouseUpAsButton(buttonInstance); });

                itemPickUp.itemSlotShowed[i].slotDisplay = buttonInstance;
                itemPickUp.itemSlotShowed[i].pickUpParent = this;
                gameObjects.Add(buttonInstance);

                slotsOnPickUpInterface.Add(buttonInstance, itemPickUp.itemSlotShowed[i]);
            }

            slotsOnPickUpInterface.UpdatePickUpSlotDisplay();
        }


    }
}


