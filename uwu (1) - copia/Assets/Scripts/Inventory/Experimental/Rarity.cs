using UnityEngine;

namespace ReLost.PlayerInventory.Items
{

    [CreateAssetMenu(fileName = "New Rarity", menuName = "Items/Rarity")]
    public class Rarity : ScriptableObject
    {
        [SerializeField] private new string name = "New Rarity Name";
        [SerializeField] private Color textColour = new Color(1f, 1f, 1f, 1f);

        public string Name { get { return name; } }
        public Color TextColour { get { return textColour; } }
    }
}
