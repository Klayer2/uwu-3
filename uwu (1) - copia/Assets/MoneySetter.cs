using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReLost.PlayerInventory.Items
{
    public class MoneySetter : MonoBehaviour
    {
        [SerializeField] private Inventory playerInventory = null;


        private void Start()
        {
            playerInventory.SetMoneyText();
        }
    }
}

