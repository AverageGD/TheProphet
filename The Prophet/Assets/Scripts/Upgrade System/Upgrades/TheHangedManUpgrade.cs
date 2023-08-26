using UnityEngine;

[CreateAssetMenu]
public class TheHangedManUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("The Hanged Man is purchased and now is working");
    }
}