using UnityEngine;

public class IsBossDeadChecker : MonoBehaviour
{
    [SerializeField] private int _bossId;

    private void Update()
    {
        if (!BossManager.instance.beatenBosses.Contains(_bossId))
            Destroy(gameObject);
    }
}
