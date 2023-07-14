using UnityEngine;

public class InventoryItemController : MonoBehaviour //This script is made for contextual iem control depending on its type
{
    public int id;

    public RibbonAbility ribbon;
    public AmuletAbility amulet;

    public void UnwearItem() //If player wants to unwear any item he needs only to click on its icon
    {
        ItemSlot itemSlot = transform.parent.gameObject.GetComponent<ItemSlot>();

        if (itemSlot == null) 
            return;

        ItemSlot.Slots slot = itemSlot.slot;


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

        switch(slots) //checks which slot player put an item to give value to the corresonding field
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
