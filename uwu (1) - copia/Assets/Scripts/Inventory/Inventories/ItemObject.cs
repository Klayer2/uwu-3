using ReLost.Inventory.Items.Hotbars;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public enum ItemType
{
    Weapon,
    Shield,
    Helmet,
    Chest,
    Boots,
    Consumable,
    Miscellaneous,
    Trash
}
namespace ReLost.Inventory.Items
{
    
    public enum Attributes
    {
        Agility,
        Intellect,
        Stamina,
        Strength
    }

    public abstract class ItemObject : ScriptableObject, IHotbarItem
    {
        [Header("Item Data")]
        public Sprite uiDisplay = null;
        public GameObject characterDisplay;
        public Rarity rarity;
        public ItemType type;
        public new string name;
        public string useText = "";
        [TextArea(15, 20)]
        public string description = "New Item Description";
        public Item data = new Item();
        [Min(0)] public int sellPrice = 1;
        [Min(0)] public int buyPrice = 1;
        [Min(1)] public int maxStack = 1;

        public List<string> boneNames = new List<string>();

        public Sprite UiDisplay => uiDisplay;

        private void OnValidate()
        {
            boneNames.Clear();
            if (characterDisplay == null)
                return;
            if (!characterDisplay.GetComponent<SkinnedMeshRenderer>())
                return;

            var renderer = characterDisplay.GetComponent<SkinnedMeshRenderer>();
            var bones = renderer.bones;

            foreach (var t in bones)
            {
                boneNames.Add(t.name);
            }

        }

        public string Name => name;
        


        public Item CreateItem()
        {
            Item newItem = new Item(this);
            return newItem;
        }
        public void Use()
        {

        }
    }

    [System.Serializable]
    public class Item
    {
        [Header("Basic Info")]
        public int Id = -1;
        public Rarity rarity;
        public string Name;
        public string useText = "";
        public Sprite uiDisplay = null;
        public ItemBuff[] buffs;
        
        public bool canBeBoughtInfinitely = false;
        public bool isUsableInfinitely = false;
        public int itemTypeID = 0;
        public int itemSubTypeID = 0;
        public int ItemSubTypeID { get { return this.itemSubTypeID; } set { this.itemSubTypeID = value; } }
        public int ItemTypeID { get { return this.itemTypeID; } set { this.itemTypeID = value; } }

        public Item()
        {
            this.Name = "";
            this.Id = -1;
        }

        public Item(ItemObject item)
        {
            Id = item.data.Id;
            Name = item.name;
            uiDisplay = item.UiDisplay;
            rarity = item.rarity;
            useText = item.useText;
            buffs = new ItemBuff[item.data.buffs.Length];
            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max)
                {
                    attribute = item.data.buffs[i].attribute
                };
            }
        }
        public string ColouredName
        {
            get
            {
                if (this.rarity == null) { return Name; }
                string hexColour = ColorUtility.ToHtmlStringRGB(rarity.textColour);
                return $"<color=#{hexColour}>{Name}</color>";
            }
        }

    }

    [System.Serializable]   
    public class ItemBuff : IModifier
    {
        public Attributes attribute;
        public int value;
        public int min;
        public int max;
        public ItemBuff(int _min, int _max)
        {
            min = _min;
            max = _max;
            GenerateValue();
        }

        public void AddValue(ref int baseValue)
        {
            baseValue += value;
        }


        public void GenerateValue()
        {
            value = Random.Range(min, max);
        }
    }
}