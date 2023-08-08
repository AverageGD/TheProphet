using UnityEngine;

public class UpgradeAbility : ScriptableObject
{
    public string name;
    public int id;

    [TextArea(3, 10)]
    public string description;
    public bool isPurchased;

    public virtual void Activate()
    {

    }
}
