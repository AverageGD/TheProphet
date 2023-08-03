using UnityEngine;

[CreateAssetMenu]
public class TheFoolUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("The Fool is purchased and now is working");
    }
}
