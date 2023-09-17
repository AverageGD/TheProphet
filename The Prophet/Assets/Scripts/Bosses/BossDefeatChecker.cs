using UnityEngine;

public class BossDefeatChecker : MonoBehaviour
{
    [SerializeField] private int _id;

    private void Start()
    {
        if (BossManager.instance.beatenBosses.Contains(_id))
            Destroy(gameObject);
    }
}
