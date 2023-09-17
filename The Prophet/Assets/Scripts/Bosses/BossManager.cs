using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    public List<int> beatenBosses = new List<int>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
