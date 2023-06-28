using UnityEngine;

[System.Serializable]
public abstract class AbstractAmulet
{
    public string ID;
    public bool isWearing;

    protected AbstractAmulet(string iD)
    {
        ID = iD;
        isWearing = false;
    }

    public abstract void AbilityInvoker();

}
