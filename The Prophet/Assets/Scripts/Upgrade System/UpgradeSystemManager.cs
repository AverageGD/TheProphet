using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystemManager : MonoBehaviour
{
    public static UpgradeSystemManager instance;

    public List<UpgradeAbility> availableUpgrades = new();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddAbility(UpgradeAbility upgradeAbility)
    {
        availableUpgrades.Add(upgradeAbility);
    }

    private void Update()
    {
        foreach (UpgradeAbility upgradeAbility in availableUpgrades)
        {
            upgradeAbility.Activate();
        }
    }
}
