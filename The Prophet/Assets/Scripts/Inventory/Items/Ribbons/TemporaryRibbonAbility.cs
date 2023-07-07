using UnityEngine;

[CreateAssetMenu]
public class TemporaryRibbonAbility : RibbonAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("Temporary Ribbon has been called");
    }
}
