using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<Item> items;

    [Header("Items' containers")]
    public Transform ribbonSlotContainer;
    public Transform amuletSlotContainer;
    public Transform keyItemSlotContainer;

    [Header("Item Prefab")]
    public GameObject itemPrefab;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _descriptionBox;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        items = new List<Item>();

    }

    public void Add(Item item) //Just adds the new item in the list
    {
        if (items == null)
            items = new List<Item>();

        items.Add(item);

        item.isWearing = false;

        SaveManager.instance.SavePlayerInventory();
    }

    public bool Contains(int id)
    {
        foreach (Item item in items)
        {
            if (item.id == id)
                return true;
        }
        return false;
    }

    public void ListItems() //This script refreshes items' list in inventory UI
    {
        //At first we destroy every item in the UI before, just to avoid duplicats

        foreach (Transform item in ribbonSlotContainer)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in amuletSlotContainer)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in keyItemSlotContainer)
        {
            Destroy(item.gameObject);
        }

        short xRibbon = 0;
        short yRibbon = 0;

        short xAmulet = 0;
        short yAmulet = 0;

        short xKeyItem = 0;
        short yKeyItem = 0;

        //Depending on item's type spawn it in the corresponding place, by changing coordiants, and give corresponding ability and other information(id, name, description, icon, etc)
        foreach (Item item in items)
        {
            GameObject obj = null;


            if (item.itemType == Item.ItemType.ribbon)
            {
                obj = Instantiate(itemPrefab, ribbonSlotContainer);

                obj.transform.localPosition = new Vector2(xRibbon * 100f, -yRibbon * 96f);
                obj.GetComponent<InventoryItemController>().ribbon = item.ribbonAbility;

                xRibbon++;
                if (xRibbon >= 1)
                {
                    xRibbon = 0;
                    yRibbon++;
                }

            }
            else if (item.itemType == Item.ItemType.amulet)
            {
                obj = Instantiate(itemPrefab, amuletSlotContainer);

                obj.transform.localPosition = new Vector2(xAmulet * 100f, -yAmulet * 96f);
                obj.GetComponent<InventoryItemController>().amulet = item.amuletAbility;

                xAmulet++;
                if (xAmulet >= 1)
                {
                    xAmulet = 0;
                    yAmulet++;
                }

            }
            else if (item.itemType == Item.ItemType.keyItem)
            {
                obj = Instantiate(itemPrefab, keyItemSlotContainer);
                obj.transform.localPosition = new Vector2(xKeyItem * 100f, -yKeyItem * 96f);

                xKeyItem++;
                if (xKeyItem >= 1)
                {
                    xKeyItem = 0;
                    yKeyItem++;
                }
            }

            InventoryItemController inventoryItemController = obj.GetComponent<InventoryItemController>();

            obj.GetComponent<DragDrop>().canvas = _canvas;
            inventoryItemController.id = item.id;
            inventoryItemController.itemName = item.name;
            inventoryItemController.itemDescription = item.description;
            inventoryItemController.descriptionBox = _descriptionBox;
            inventoryItemController.isWearing = item.isWearing;
            Image itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemIcon.sprite = item.icon;
        }

    }

    public void ResetWearedIems(int id, bool condition)
    {
        foreach (Item item in items)
        {
            if (item.id == id)
            {
                item.isWearing = condition;
            }
        }
        ListItems();
    }
}