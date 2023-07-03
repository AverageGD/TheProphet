using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    public List <AbstractAmulet> amulets = new List <AbstractAmulet>();
    public List<AbstractRibbon> ribbons = new List <AbstractRibbon>();
    public List<AbstractKeyItem> keyItems = new List <AbstractKeyItem>();

    public void Start()
    {
        instance = this;
    }
    public void AddAmulet(AbstractAmulet amulet)
    {
        amulets.Add(amulet);
    }

    public void AddRibbon(AbstractRibbon ribbon)
    {
        ribbons.Add(ribbon);
    }

    public void AddKeyItem(AbstractKeyItem keyItem)
    {
        keyItems.Add(keyItem);
    }

    public void UseRibbon()
    {
        foreach(AbstractRibbon ribbon in ribbons)
        {
            if (ribbon.isWearing == true)
            {
                ribbon.AbilityInvoker();
                break;
            }
        }
    }

}
