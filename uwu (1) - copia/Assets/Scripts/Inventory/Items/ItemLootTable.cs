using ReLost.Interactables;
using ReLost.PlayerNameSpace;
using UnityEditor;
using UnityEngine;

namespace ReLost.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Loot Table", menuName = "Inventory System/Loot Table")]
    public class ItemLootTable : ScriptableObject
    {
        public ItemDrop[] itemDrops = new ItemDrop[8];
        public int minMoney = 1;
        public int maxMoney = 1;
        
    }
    [System.Serializable]
    public class ItemDrop
    {
        public ItemObject itemObject;
        public float chanceOutOfOne = 0;
        public int maxAmountToBeDropped = 1;
    }
}

