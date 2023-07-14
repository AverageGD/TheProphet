using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List <Item> items = new List <Item>();

    [Header("Items' containers")]
    public Transform ribbonSlotContainer;
    public Transform amuletSlotContainer;
    public Transform keyItemSlotContainer;

    [Header("Item Prefab")]
    public GameObject itemPrefab;

    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
    }


    public void ListItems()
    {
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

        foreach (Item item in items)
        {
            GameObject obj = null;
            if (item.itemType == Item.ItemType.ribbon)
            {
                obj = Instantiate(itemPrefab, ribbonSlotContainer);

                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xRibbon * 100f, -yRibbon * 100f);
                obj.GetComponent<InventoryItemController>().ribbon = item.ribbonAbility;

                xRibbon++;
                if (xRibbon > 1)
                {
                    xRibbon = 0;
                    yRibbon++;
                }

            } else if (item.itemType == Item.ItemType.amulet)
            {
                obj = Instantiate(itemPrefab, amuletSlotContainer);

                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xAmulet * 100f, -yAmulet * 100f);
                obj.GetComponent<InventoryItemController>().amulet = item.amuletAbility;

                xAmulet++;
                if (xAmulet > 1)
                {
                    xAmulet = 0;
                    yAmulet++;
                }

            } else if (item.itemType == Item.ItemType.keyItem)
            {
                obj = Instantiate(itemPrefab, keyItemSlotContainer);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xKeyItem * 100f, -yKeyItem * 100f);

                xKeyItem++;
                if (xKeyItem > 1)
                {
                    xKeyItem = 0;
                    yKeyItem++;
                }
            }

            obj.GetComponent<DragDrop>().canvas = _canvas;
            obj.GetComponent<InventoryItemController>().id = item.id;

            TMP_Text itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            Image itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();


            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

    }
}
