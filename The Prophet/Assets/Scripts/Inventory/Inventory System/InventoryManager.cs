using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List <Item> items = new List <Item>();

    [Header("Items' contents")]
    public Transform ribbonContent;
    public Transform amuletContent;
    public Transform keyItemContent;

    [Header("Weared Items' content")]
    public Transform wearedRibbonContent;
    public Transform wearedAmuletContent;

    [Header("Item Prefab")]
    public GameObject itemPrefab;

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
        foreach (Transform item in ribbonContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in amuletContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in keyItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Item item in items)
        {
            GameObject obj = null;
            if (item.itemType == Item.ItemType.ribbon)
            {
                obj = Instantiate(itemPrefab, ribbonContent);
                obj.GetComponent<InventoryItemController>().wearedRibbonContent = wearedRibbonContent;
                obj.GetComponent<InventoryItemController>().ribbon = item.ribbonAbility;

            } else if (item.itemType == Item.ItemType.amulet)
            {
                obj = Instantiate(itemPrefab, amuletContent);
                obj.GetComponent<InventoryItemController>().wearedAmuletContent = wearedAmuletContent;
                obj.GetComponent<InventoryItemController>().amulet = item.amuletAbility;

            } else if (item.itemType == Item.ItemType.keyItem)
            {
                obj = Instantiate(itemPrefab, keyItemContent);
            }

            TMP_Text itemName = obj.transform.Find("ItemName").GetComponent<TMP_Text>();
            Image itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();


            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

    }
}
