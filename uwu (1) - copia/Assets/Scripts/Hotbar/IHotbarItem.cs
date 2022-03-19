using UnityEngine;

namespace ReLost.Inventory.Items.Hotbars
{
    public interface IHotbarItem
    {

        string Name { get; }

        Sprite UiDisplay { get; }

        void Use();

    }
}
