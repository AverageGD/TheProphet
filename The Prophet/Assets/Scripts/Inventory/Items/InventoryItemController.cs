using UnityEngine;

public class InventoryItemController : MonoBehaviour //This script is made for contextual iem control depending on its type
{
    public RibbonAbility ribbon;

    public void WearRibbon() //If player wants to wear any ribbon he needs only to click on its icon
    {
        RibbonHolder.instance.ribbon = ribbon;
    }
}
