using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    public Dictionary<string, AbstractAmulet> amulets = new Dictionary<string, AbstractAmulet>();
    public Dictionary<string, AbstractRibbon> ribbons = new Dictionary<string, AbstractRibbon>();
    public Dictionary<string, AbstractKeyItem> keyItems = new Dictionary<string, AbstractKeyItem>();

    public void Start()
    {
    }

    public void AddAmulet(AbstractAmulet amulet, string ID)
    {
        amulets.Add(ID, amulet);
    }

    public void AddRibbon(AbstractRibbon ribbon, string ID)
    {
        ribbons.Add(ID, ribbon);
    }

    public void AddKeyItem(AbstractKeyItem keyItem, string ID)
    {
        keyItems.Add(ID, keyItem);
    }

    public void UseRibbon()
    {
        foreach (KeyValuePair <string, AbstractRibbon> ribbon in ribbons)
        {
            if (ribbon.Value.isWearing == true)
            {
                ribbon.Value.AbilityInvoker();
                break;
            }
        }
    }

    public void WearRibbon(string wearRibbon)
    {
        foreach (KeyValuePair<string, AbstractRibbon> ribbon in ribbons)
        {
            if (ribbon.Value.isWearing == true)
            {
                ribbon.Value.isWearing = false;
                ribbons[wearRibbon].isWearing = true;
                break;
            }
        }
    }

    public void WearAmulet(string wearAmulet, string unwearAmulet)
    {
        amulets[wearAmulet].isWearing = true;
        amulets[unwearAmulet].isWearing = false;
    }

}

