using UnityEngine;

public class InventoryItemController : MonoBehaviour //This script is made for contextual iem control depending on its type
{
    public RibbonAbility ribbon;
    public Transform wearedRibbonContent;

    public AmuletAbility amulet;
    public Transform wearedAmuletContent;

    private short replacingAmuletNumber = 0;
    public void WearItem() //If player wants to wear any item he needs only to click on its icon
    {
        if (ribbon != null) WearRibbon();

        if (amulet != null) WearAmulet();
    }

    private void WearRibbon()
    {
        foreach (Transform item in wearedRibbonContent)
        {
            Destroy(item.gameObject);
        }

        Instantiate(gameObject, wearedRibbonContent);
        RibbonHolder.instance.ribbon = ribbon;
    }

    private void WearAmulet()
    {
        if (wearedAmuletContent.childCount > 0)
            Destroy(wearedAmuletContent.GetChild(replacingAmuletNumber).gameObject);


        replacingAmuletNumber++;
        replacingAmuletNumber %= 2;
        Instantiate(gameObject, wearedAmuletContent);

        if (replacingAmuletNumber == 0) 
            AmuletHolder.instance.firstAmuletAbility = amulet;

        else 
            AmuletHolder.instance.secondAmuletAbility = amulet;

    }
}
