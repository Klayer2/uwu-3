using UnityEngine;

namespace ReLost.PlayerInventory.Items
{

    [CreateAssetMenu(fileName = "New RarityList", menuName = "Items/RarityList")]
    public class RarityList : ScriptableObject
    {
        public Rarity[] rarityList;
    }
}
