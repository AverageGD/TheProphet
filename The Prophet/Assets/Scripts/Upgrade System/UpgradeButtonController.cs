using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonController : MonoBehaviour
{
    [SerializeField] private UpgradeButtonController _previousButton;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private UpgradeAbility _upgradeAbility;



    public bool isPurchased = false;

    private void Start()
    {
        transform.Find("Name").GetComponent<Text>().text = _upgradeAbility.name;
    }

    public void TryToPurchaseUpgrade()
    {
        if (PlayerCurrencyController.instance.currency - _upgradeCost >= 0 && (_previousButton == null || _previousButton.isPurchased) && !isPurchased)
        {
            isPurchased = true;
            PlayerCurrencyController.instance.TakeCurrency(_upgradeCost);
            UpgradeSystemManager.instance.AddAbility(_upgradeAbility);
        }
    }



}
