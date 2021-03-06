using ReLost.Inventory.Items.Hotbars;
using UnityEngine;

namespace ReLost.Magic
{

    public class Spell : ScriptableObject, IHotbarItem
    {

        [Header("Basic Info")]
        [SerializeField] private new string name = "New Spell Name";
        [SerializeField] private Sprite icon = null;
        [SerializeField] private Element element = null;

        public string Name => name;

        public Sprite UiDisplay => UiDisplay;
        public Element Element => element;

        public void Use()
        {
            Debug.Log($"Casting {Name}");
        }

    }


}
