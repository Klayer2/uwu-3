using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.Inventory.Items
{
    public class StaticInterface : UserInterface
    {
        public GameObject[] slots;

        public override void CreateSlots()
        {
            slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
            for (int i = 0; i < inventory.GetSlots.Length; i++)
            {
                var buttonInstance = slots[i];


                AddEvent(buttonInstance, EventTriggerType.PointerEnter, delegate { OnEnter(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.PointerExit, delegate { OnExit(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.BeginDrag, delegate { OnDragStart(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.EndDrag, delegate { OnDragEnd(buttonInstance); });
                AddEvent(buttonInstance, EventTriggerType.Drag, delegate { OnDrag(buttonInstance); });
                inventory.GetSlots[i].slotDisplay = buttonInstance;
                slotsOnInterface.Add(buttonInstance, inventory.GetSlots[i]);

            }
        }
    }
}
