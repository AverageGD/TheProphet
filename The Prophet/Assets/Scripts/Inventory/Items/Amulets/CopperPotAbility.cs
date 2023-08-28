using UnityEngine;

[CreateAssetMenu]
public class CopperPotAbility : AmuletAbility
{
    public override void Activate()
    {
        base.Activate();

        PlayerHealthController.instance.maxHealth *= 1.05f;
        PlayerHealthController.instance.SetHealth(PlayerHealthController.instance.maxHealth);
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerHealthController.instance.maxHealth /= 1.05f;
        PlayerHealthController.instance.SetHealth(PlayerHealthController.instance.maxHealth);
    }
}