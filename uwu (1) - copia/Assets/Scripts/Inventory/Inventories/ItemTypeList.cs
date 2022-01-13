using UnityEngine;

namespace ReLost.PlayerInventory.Items 
{
    [CreateAssetMenu(fileName = "New ItemType List", menuName = "Items/ItemTypeList")]
    public class ItemTypeList : ScriptableObject
    {       
        public int[] ItemType;
        public int[] ItemSubType;
    }

}
