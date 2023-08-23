using UnityEngine;

public class PlayerCurrencyController : MonoBehaviour
{

    public static PlayerCurrencyController instance;

    public int currency;
    public float currencyMultiplier;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddCurrency(int currencyCount)
    {
        currency += currencyCount;
        SaveManager.instance.SavePlayerCurrency();
        CurrencyUIController.instance.UpdateCurrencyWindowValueInvoker(1);
    }

    public void TakeCurrency(int currencyCount)
    {
        if (currency - currencyCount >= 0)
            currency -= currencyCount;
        CurrencyUIController.instance.UpdateCurrencyWindowValueInvoker(-1);
    }
}
