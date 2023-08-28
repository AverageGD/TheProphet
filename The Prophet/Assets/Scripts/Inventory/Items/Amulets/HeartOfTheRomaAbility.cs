using UnityEngine;

[CreateAssetMenu]
public class HeartOfTheRomaAbility : AmuletAbility
{
    public override void Activate()
    {
        base.Activate();

        PlayerHealthController.instance.isHeartOfTheRomaUsed = true;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerHealthController.instance.isHeartOfTheRomaUsed = false;
    }
}
