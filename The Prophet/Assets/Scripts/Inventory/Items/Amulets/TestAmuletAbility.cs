using UnityEngine;

[CreateAssetMenu]
public class TestAmuletAbility : AmuletAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("Test Amulet is working now");
    }
}
