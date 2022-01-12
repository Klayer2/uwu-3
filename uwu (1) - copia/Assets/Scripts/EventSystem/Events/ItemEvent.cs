using ReLost.PlayerInventory.Items;
using UnityEngine;

namespace ReLost.Events
{
    [CreateAssetMenu(fileName = "New Item Event", menuName = "GameEvents/Item Event")]
    public class ItemEvent : BaseGameEvent<Item> { }
}