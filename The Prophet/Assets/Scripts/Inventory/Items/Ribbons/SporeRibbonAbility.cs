using UnityEngine;

[CreateAssetMenu]
public class SporeRibbonAbility : RibbonAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("Spore Ribbon has been called");
    }
}
