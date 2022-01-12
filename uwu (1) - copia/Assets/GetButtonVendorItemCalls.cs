using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReLost.NPCs.Occupations.Vendors
{
    public class GetButtonVendorItemCalls : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private VendorItemButton thisVendorItemButton = null;

            public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                thisVendorItemButton.BuyOrSellCaller();
            }
        }
    }
    
}


