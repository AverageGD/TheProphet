using UnityEngine;

[System.Serializable]
public abstract class AbstractRibbon
{
    public string ID;
    public bool isWearing;

    protected AbstractRibbon(string iD)
    {
        ID = iD;
        isWearing = false;
    }

    public abstract void AbilityInvoker();
}
