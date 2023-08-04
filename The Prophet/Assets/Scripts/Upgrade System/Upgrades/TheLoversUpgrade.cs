using UnityEngine;

[CreateAssetMenu]
public class TheLoversUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("The Lovers are purchased and now are working");
    }
}
