using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour //This script is made for contextual iem control depending on its type
{
    public int id;
    public bool isWearing;

    public RibbonAbility ribbon;
    public AmuletAbility amulet;

    public string itemName;
    public string itemDescription;

    public GameObject descriptionBox;

    public void TryToUnwearItemAndShowItemDescription() //If player wants to unwear any item or if he wants to read its description he needs only to click on its icon
    {
        ItemSlot itemSlot = transform.parent.gameObject.GetComponent<ItemSlot>();

        descriptionBox.SetActive(true);

        descriptionBox.transform.Find("Name").GetComponent<Text>().text = itemName;
        descriptionBox.transform.Find("Description").GetComponent<Text>().text = itemDescription;

        if (itemSlot == null)
            return;

        amulet?.Deactivate();

        ItemSlot.Slots slot = itemSlot.slot;
        isWearing = false;
        InventoryManager.instance.ResetWearedIems(id, false);

        if (slot == ItemSlot.Slots.ribbon1)
        {
            RibbonHolder.instance.ribbonAbility = null;
            Destroy(transform.gameObject);
            return;
        }

        if (slot == ItemSlot.Slots.amulet1)
        {
            AmuletHolder.instance.firstAmuletAbility = null;
            Destroy(transform.gameObject);
            return;
        }

        if (slot == ItemSlot.Slots.amulet2)
        {
            AmuletHolder.instance.secondAmuletAbility = null;
            Destroy(transform.gameObject);
            return;
        }
    }

    public void WearItem(ItemSlot.Slots slots) //if player wants to wear any item he needs only drag it to the corresponding slot
    {

        switch (slots) //checks which slot player put an item to give value to the corresonding field
        {
            case ItemSlot.Slots.ribbon1:
                RibbonHolder.instance.ribbonAbility = ribbon;
                break;
            case ItemSlot.Slots.amulet1:
                AmuletHolder.instance.firstAmuletAbility = amulet;
                break;
            case ItemSlot.Slots.amulet2:
                AmuletHolder.instance.secondAmuletAbility = amulet;
                break;
            default:
                break;

        }
    }

}