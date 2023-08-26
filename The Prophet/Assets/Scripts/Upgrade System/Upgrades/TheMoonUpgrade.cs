using UnityEngine;

[CreateAssetMenu]
public class TheMoonUpgrade : UpgradeAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("The Moon is purchased and now is working");

        PlayerManaController.instance.maxMana *= 1.15f;
        PlayerManaController.instance.ReceiveMana(PlayerManaController.instance.maxMana);
    }
}