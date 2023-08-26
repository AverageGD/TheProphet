using UnityEngine;

[CreateAssetMenu]
public class TheHermitUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("The Hermit is purchased and now is working");
    }
}
