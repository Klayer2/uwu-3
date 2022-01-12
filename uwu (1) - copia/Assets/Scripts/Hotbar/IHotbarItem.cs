using UnityEngine;

namespace ReLost.PlayerInventory.Items.Hotbars
{
    public interface IHotbarItem
    {

        string Name { get; }

        Sprite Icon { get; }

        void Use();

    }
}
