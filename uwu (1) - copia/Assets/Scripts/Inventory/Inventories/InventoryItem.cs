using ReLost.PlayerInventory.Items.Hotbars;
using System.Text;
using UnityEngine;

namespace ReLost.PlayerInventory.Items
{

    [CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
    public class InventoryItem : Item, IHotbarItem
    {
        [Header("Item Data")]
        [SerializeField] private Rarity rarity = null;
        [SerializeField] [Min(0)] private int sellPrice = 1;
        [SerializeField] [Min(0)] private int buyPrice = 1;
        [SerializeField] [Min(1)] private int maxStack = 5;
        [SerializeField] private bool isUnlimited = false;
        [SerializeField] private bool isUsableInfinitely = false;
        [Header("1 = Miscellaneous, 2 = Consumable")]
        [SerializeField] private int itemType = 0;
        [SerializeField] private string useText = "Does something, Maybe?";
        public override string ColouredName
        {
            get
            {
                string hexColour = ColorUtility.ToHtmlStringRGB(rarity.TextColour);
                return $"<color=#{hexColour}>{Name}</color>";
            }
        }
        public int SellPrice => sellPrice;

        public int ItemType => itemType;

        public int BuyPrice => buyPrice;

        public int MaxStack => maxStack;

        public bool IsUnlimited => isUnlimited;
        public bool IsUsableInfinitely => isUsableInfinitely;

        public Rarity Rarity => rarity;

        

        public override string GetInfoDisplayText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("<color=yellow>Use: ").Append(useText).Append("</color>").AppendLine();
            builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
            builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");

            return builder.ToString();
        }

        public void Use()
        {
            Debug.Log($"Drinking {Name}");
        }
    }
}