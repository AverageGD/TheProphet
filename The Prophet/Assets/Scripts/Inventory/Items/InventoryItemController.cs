using UnityEngine;

public class InventoryItemController : MonoBehaviour //This script is made for contextual iem control depending on its type
{
    public RibbonAbility ribbon;
    public Transform wearedRibbonSlotContainer;

    public AmuletAbility amulet;
    public Transform wearedAmuletSlotContainer;

    public void WearItem() //If player wants to wear any item he needs only to click on its icon
    {
        if (ribbon != null) WearRibbon();

        if (amulet != null) WearAmulet();
    }

    private void WearRibbon()
    {
        foreach (Transform item in wearedRibbonSlotContainer)
        {
            Destroy(item.gameObject);
        }

        Instantiate(gameObject, wearedRibbonSlotContainer);
        RibbonHolder.instance.ribbon = ribbon;
    }

    private void WearAmulet()
    {
        

    }
}
