using UnityEngine;

public class RibbonAbility : ScriptableObject
{
    public string name;
    public float coolDownTime;
    public float activeTime;

    public virtual void Activate() 
    {
    }
}
