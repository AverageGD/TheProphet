using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonController : MonoBehaviour
{
    [SerializeField] private UpgradeButtonController _previousButton;
    [SerializeField] private int _upgradeCost;
    [SerializeField] private UpgradeAbility _upgradeAbility;

    [SerializeField] Sprite _lockedUpgrade;
    [SerializeField] Sprite _purchasedUpgrade;
    [SerializeField] Sprite _tarotCard;

    public bool isPurchased = false;

    private void Update()
    {
        isPurchased = UpgradeSystemManager.instance.CanUseAbility(_upgradeAbility.name);
        if (isPurchased)
        {
            _upgradeAbility.isPurchased = true;
            GetComponent<Image>().sprite = _purchasedUpgrade;
        } else
        {
            _upgradeAbility.isPurchased = false;
            GetComponent<Image>().sprite = _lockedUpgrade;
        }
    }

    public void TryToPurchaseUpgrade()
    {
        if (transform.parent.parent.Find("TarotCard").gameObject.GetComponent<Image>().sprite != _tarotCard)
            return;

        if (PlayerCurrencyController.instance.currency - _upgradeCost >= 0 && (_previousButton == null || _previousButton.isPurchased) && !isPurchased)
        {
            _upgradeAbility.isPurchased = true;

            GetComponent<Image>().sprite = _purchasedUpgrade;
            PlayerCurrencyController.instance.TakeCurrency(_upgradeCost);
            UpgradeSystemManager.instance.AddAbility(_upgradeAbility);
        }
    }

    public void ShowInformationAboutUpgrade()
    {
        transform.parent.parent.Find("DescriptionBox").Find("Name").gameObject.GetComponent<Text>().text = _upgradeAbility.name;
        transform.parent.parent.Find("DescriptionBox").Find("Description").gameObject.GetComponent<Text>().text = _upgradeAbility.description;
        transform.parent.parent.Find("DescriptionBox").Find("Price").gameObject.GetComponent<Text>().text = "Price: " + _upgradeCost.ToString();

        transform.parent.parent.Find("TarotCard").gameObject.GetComponent<Image>().sprite = _tarotCard;
    }



}
