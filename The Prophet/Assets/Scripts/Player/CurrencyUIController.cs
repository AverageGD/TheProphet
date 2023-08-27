using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUIController : MonoBehaviour
{
    public static CurrencyUIController instance;

    private Text currencyWindowText;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        currencyWindowText = transform.Find("Currency").GetComponent<Text>();
        currencyWindowText.text = Convert.ToString(0);
    }

    public void UpdateCurrencyWindowValueInvoker()
    {
        StopAllCoroutines();

        StartCoroutine(UpdateCurrencyWindowValue());
    }

    private IEnumerator UpdateCurrencyWindowValue()
    {
        int newCurrency = PlayerCurrencyController.instance.currency;

        while (Convert.ToInt16(currencyWindowText.text) < newCurrency)
        {
            currencyWindowText.text = Convert.ToString((Convert.ToInt16(currencyWindowText.text) + 1));

            yield return new WaitForSeconds(0.01f);
        }

        while (Convert.ToInt16(currencyWindowText.text) > newCurrency)
        {
            currencyWindowText.text = Convert.ToString((Convert.ToInt16(currencyWindowText.text) - 1));

            yield return new WaitForSeconds(0.01f);
        }
    }
}
