using ReLost.Inventory;
using ReLost.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ReLost.PlayerNameSpace
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TMP_InputField itemFindText = null;
        [SerializeField] private TextMeshProUGUI moneyText = null;
        [SerializeField] private bool isVendor;
        
        public Attribute[] attributes;
        public InventoryObject inventory;
        public InventoryObject equipment;
        public bool isFirstLoad = true;

        private BoneCombiner boneCombiner;

        private Transform boots;
        private Transform chest;
        private Transform helmet;
        private Transform offhand;
        private Transform sword;

        public Transform weaponTransform;
        public Transform offhandWristTransform;
        public Transform offhandHandTransform;

        private int itemTypeEnumCount = System.Enum.GetNames(typeof(ItemType)).Length;

        private void OnApplicationQuit()
        {
            inventory.Save();
            inventory.EmptyInventory();
        }

        private void Start()
        {
            boneCombiner = new BoneCombiner(gameObject);

            if (isFirstLoad == false)
            {
                inventory.Load();
            }
            else
            {
                isFirstLoad = false;
            }

            for (int i = 0; i < inventory.container.itemSlots.Length; i++)
            {
                if (inventory.container.itemSlots[i].item.rarity == null)
                {
                    inventory.container.itemSlots[i].amount = 0;
                }
            }

            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].SetParent(this);
            }

            for (int i = 0; i < equipment.GetSlots.Length; i++)
            {
                equipment.GetSlots[i].OnBeforeUpdate += OnRemoveItem;
                equipment.GetSlots[i].OnAfterUpdate += OnAddItem;
            }
        }

        public void OnRemoveItem(InventorySlot _slot)
        {
            if (_slot.ItemObject == null)
                return;
            switch (_slot.parent.inventory.type)
            {
                case InterfaceType.Inventory:
                    break;
                case InterfaceType.Equipment:
                    print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type,
                        ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                    for (int i = 0; i < _slot.item.buffs.Length; i++)
                    {
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].type == _slot.item.buffs[i].attribute)
                                attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                        }
                    }

                    if (_slot.ItemObject.characterDisplay != null)
                    {
                        switch (_slot.AllowedItems[0])
                        {
                            case ItemType.Helmet:
                                Destroy(helmet.gameObject);
                                break;
                            case ItemType.Weapon:
                                Destroy(sword.gameObject);
                                break;
                            case ItemType.Shield:
                                Destroy(offhand.gameObject);
                                break;
                            case ItemType.Boots:
                                Destroy(boots.gameObject);
                                break;
                            case ItemType.Chest:
                                Destroy(chest.gameObject);
                                break;
                        }
                    }

                    break;
                default:
                    break;
            }
        }

        public void OnAddItem(InventorySlot _slot)
        {
            if (_slot.ItemObject == null)
                return;
            switch (_slot.parent.inventory.type)
            {
                case InterfaceType.Inventory:
                    break;
                case InterfaceType.Equipment:
                    print(
                        $"Placed {_slot.ItemObject}  on {_slot.parent.inventory.type}, Allowed Items: {string.Join(", ", _slot.AllowedItems)}");

                    for (int i = 0; i < _slot.item.buffs.Length; i++)
                    {
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].type == _slot.item.buffs[i].attribute)
                                attributes[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }

                    if (_slot.ItemObject.characterDisplay != null)
                    {
                        switch (_slot.AllowedItems[0])
                        {
                            case ItemType.Helmet:
                                helmet = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay,
                                    _slot.ItemObject.boneNames);
                                break;
                            case ItemType.Weapon:
                                sword = Instantiate(_slot.ItemObject.characterDisplay, weaponTransform).transform;
                                break;
                            case ItemType.Shield:
                                switch (_slot.ItemObject.type)
                                {
                                    case ItemType.Weapon:
                                        offhand = Instantiate(_slot.ItemObject.characterDisplay, offhandHandTransform)
                                            .transform;
                                        break;
                                    case ItemType.Shield:
                                        offhand = Instantiate(_slot.ItemObject.characterDisplay, offhandWristTransform)
                                            .transform;
                                        break;
                                }

                                break;
                            case ItemType.Boots:
                                boots = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay, _slot.ItemObject.boneNames);
                                break;
                            case ItemType.Chest:
                                chest = boneCombiner.AddLimb(_slot.ItemObject.characterDisplay, _slot.ItemObject.boneNames);
                                break;
                        }
                    }


                    break;
                default:
                    break;
            }
        }

        public void AddMoney(int moneyIn)
        {
            inventory.container.money += moneyIn;
            moneyText.text = $"{inventory.container.money} <sprite=0>";
        }

        public void ReduceMoney(int moneyOut)
        {
            inventory.container.money -= moneyOut;
            moneyText.text = $"{inventory.container.money} <sprite=0>";
        }

        public void SetMoneyText()
        {
            moneyText.text = $"{inventory.container.money} <sprite=0>";
        }

        public void SearchItem()
        {
            var ItemFindText = itemFindText.text.ToLower();
            if (itemFindText == null) { return; }
            for (int i = 0; i < inventory.container.itemSlots.Length; i++)
            {
                if (itemFindText.text == "") { inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(false); continue; }
                if (inventory.container.itemSlots[i].item == null) { continue; }

                if (inventory.container.itemSlots[i].item.Name.ToLower().Contains(ItemFindText))
                {
                    inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                if (inventory.container.itemSlots[i].item.rarity.name.ToLower() == (ItemFindText))
                {
                    inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }
        public void AttributeModified(Attribute attribute)
        {
            Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
        }
    }

    [System.Serializable]
    public class Attribute
    {
        [System.NonSerialized] public Player parent;
        public Attributes type;
        public ModifiableInt value;

        public void SetParent(Player _parent)
        {
            parent = _parent;
            value = new ModifiableInt(AttributeModified);
        }

        public void AttributeModified()
        {
            parent.AttributeModified(this);
        }
    }
}

