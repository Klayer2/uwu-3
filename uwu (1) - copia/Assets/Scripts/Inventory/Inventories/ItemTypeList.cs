using UnityEngine;

namespace ReLost.Inventory.Items 
{
    [CreateAssetMenu(fileName = "New ItemType List", menuName = "Inventory System/Items/ItemTypeList")]
    public class ItemTypeList : ScriptableObject
    {
        public int[] ItemSubType;
    }

}
