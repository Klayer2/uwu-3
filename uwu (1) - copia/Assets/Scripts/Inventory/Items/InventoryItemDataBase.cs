using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ReLost.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Inventory Item Database", menuName = "Inventory System/Items/Items DataBase")]
    public class InventoryItemDataBase : ScriptableObject, ISerializationCallbackReceiver
    {
        public ItemObject[] ItemObjects;

        [ContextMenu("Update ID's")]
        public void UpdateID()
        {
            for (int i = 0; i < ItemObjects.Length; i++)
            {
                if (ItemObjects[i].data.Id != i)
                    ItemObjects[i].data.Id = i;
            }
        }

        public void OnAfterDeserialize()
        {
            UpdateID();
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
