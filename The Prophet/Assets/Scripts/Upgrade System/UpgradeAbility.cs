using UnityEngine;

public class UpgradeAbility : ScriptableObject
{
    public string name;

    [TextArea(3, 10)]
    public string description;

    public virtual void Activate()
    {

    }
}
