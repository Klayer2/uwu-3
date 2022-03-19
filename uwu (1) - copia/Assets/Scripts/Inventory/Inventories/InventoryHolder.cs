using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace ReLost.Inventory.Items
{

    public class InventoryHolder : MonoBehaviour
    {
        [SerializeField] private TMP_InputField itemFindText = null;
        [SerializeField] private TextMeshProUGUI moneyText = null;
        [SerializeField] InventoryObject inventory;
        [SerializeField] private bool isVendor;
        private int itemTypeEnumCount = ItemType.GetNames(typeof(ItemType)).Length;
        private InventoryItemDataBase dataBase;


        public InventoryObject Inventory => inventory;

        private void Awake()
        {
#if UNITY_EDITOR
            dataBase = (InventoryItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Items/DataBase/Item Database.asset", typeof(InventoryItemDataBase));
#else
            dataBase = Resources.Load<InventoryItemDataBase>("Items/DataBase/Item Database");
#endif

            for (int i = 0; i < inventory.container.itemSlots.Length; i++)
            {
                if (inventory.container.itemSlots[i].item == null)
                {
                    inventory.container.itemSlots[i].amount = 0;
                }
            }
            if (!this.gameObject.CompareTag("Player"))
            {
                inventory.ByRaritySorter();
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
                if (inventory.container.itemSlots[i].item.Id < 0) { continue; }

                if (dataBase.ItemObjects[inventory.container.itemSlots[i].item.Id].Name.ToLower().Contains(ItemFindText))
                {
                    inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                if (dataBase.ItemObjects[inventory.container.itemSlots[i].item.Id].rarity.name.ToLower() == (ItemFindText))
                {
                    inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(true);
                }
                else
                {
                    inventory.container.itemSlots[i].slotDisplay.transform.GetChild(2).gameObject.SetActive(false);
                }
            }
        }

    }
}