using UnityEngine;

public class RibbonAbility : ScriptableObject //Parent for all ribbon abilities which we connect to the corresponding items scriptable objects
{
    public string name;
    public float coolDownTime;
    public float activeTime;

    public virtual void Activate() 
    {
    }
}
