using UnityEngine;

[System.Serializable]
public abstract class AbstractKeyItem
{
    public string ID;

    protected AbstractKeyItem(string iD)
    {
        ID = iD;
    }
}
