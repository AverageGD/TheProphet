using UnityEngine;

public class PlayerCurrencyController : MonoBehaviour
{

    public static PlayerCurrencyController instance;

    public int currency;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void AddCurrency(int currencyCount)
    {
        currency += currencyCount;
    }

    public void TakeCurrency(int currencyCount)
    {
        if (currency - currencyCount >= 0)
            currency -= currencyCount;
    }
}
