using ReLost.Inventory.Items;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace ReLost.Inventory
{
    public class DisplayInventorySorted : MonoBehaviour
    {
        public DynamicInterface dynamicInterface;

        private void OnEnable()
        {
            dynamicInterface = transform.GetComponentInParent<DynamicInterface>();
            dynamicInterface.ByRaritySorter();
        }

        private void OnDisable()
        {
            
        }

        
    }
}


