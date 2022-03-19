using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ReLost.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory System/Items/ItemTypes/Consumable Item")]
    public class ConsumableItem : ItemObject
    {
        

        private void Awake()
        {
            useText = "Does something, Maybe?";
            type = ItemType.Consumable;
            data.ItemTypeID = (int)type;
        }

    }
}
