using ReLost.Inventory.Items;
using UnityEngine;

namespace ReLost.Events
{
    [CreateAssetMenu(fileName = "New Item Object Event", menuName = "GameEvents/ItemObject Event")]
    public class ItemEvent : BaseGameEvent<Item> { }
}