using UnityEngine;

[CreateAssetMenu]
public class GoldGobletAbility : AmuletAbility
{
    public override void Activate()
    {
        base.Activate();

        PlayerCurrencyController.instance.currencyMultiplier *= 1.5f;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        PlayerCurrencyController.instance.currencyMultiplier /= 1.5f;
    }
}
