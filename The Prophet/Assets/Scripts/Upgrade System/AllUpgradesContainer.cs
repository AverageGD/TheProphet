using System.Collections.Generic;
using UnityEngine;

public class AllUpgradesContainer : MonoBehaviour
{
    public static AllUpgradesContainer instance;

    [SerializeField] private List<UpgradeAbility> upgrades = new List<UpgradeAbility>();

    public Dictionary<int, UpgradeAbility> upgradesDictionary = new Dictionary<int, UpgradeAbility>();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (UpgradeAbility upgrade in upgrades)
        {
            upgradesDictionary[upgrade.id] = upgrade;
            upgrade.isPurchased = false;
        }
    }
}
