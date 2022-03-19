using ReLost.Inventory.Items;
using UnityEngine;

namespace ReLost.Events
{
    [CreateAssetMenu(fileName = "New Item Container Event", menuName = "GameEvents/Item Container Event")]
    public class ItemContainerEvent : BaseGameEvent<IItemContainer> { }
}