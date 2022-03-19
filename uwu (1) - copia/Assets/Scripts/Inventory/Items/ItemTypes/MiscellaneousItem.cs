using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace ReLost.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Miscellaneos Item", menuName = "Inventory System/Items/ItemTypes/Miscellaneous Item")]
    public class MiscellaneousItem : ItemObject
    {
        private void Awake()
        {
            type = ItemType.Miscellaneous;
            data.ItemTypeID = (int)type;
        }

    }
}
