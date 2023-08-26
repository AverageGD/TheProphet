using UnityEngine;

[CreateAssetMenu]
public class TheSunUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("The Sun is purchased and now is working");

        PlayerHealthController.instance.maxHealth *= 1.15f;
        PlayerHealthController.instance.SetHealth(PlayerHealthController.instance.maxHealth);
    }
}