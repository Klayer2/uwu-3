using ReLost.PlayerNameSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReLost.Inventory.Items
{
    public class MoneySetter : MonoBehaviour
    {
        [SerializeField] private Player playerInventory = null;

        private void Awake()
        {
            playerInventory = FindObjectOfType<Player>();
        }

        private void Start()
        {
            
            playerInventory.SetMoneyText();
        }
    }
}

