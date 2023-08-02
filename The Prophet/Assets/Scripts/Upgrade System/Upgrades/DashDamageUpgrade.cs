using UnityEngine;

[CreateAssetMenu]
public class DashDamageUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("DashDamageAbility is purchased and now is working");
    }
}
