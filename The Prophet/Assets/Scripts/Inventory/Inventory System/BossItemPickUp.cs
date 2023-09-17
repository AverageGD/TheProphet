using UnityEngine;

public class BossItemPickUp : ItemPickUp
{
    [SerializeField] private int _bossId;

    protected override void Update()
    {
        base.Update();

        if (!BossManager.instance.beatenBosses.Contains(_bossId))
            Destroy(gameObject);
    }
}
