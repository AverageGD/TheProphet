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

    public void UpdateCurrencyWindowValueInvoker(int n)
    {
        StopAllCoroutines();

        StartCoroutine(UpdateCurrencyWindowValue(n));
    }

    private IEnumerator UpdateCurrencyWindowValue(int n)
    {
        int newCurrency = PlayerCurrencyController.instance.currency;

        while (Convert.ToInt16(currencyWindowText.text) != newCurrency)
        {
            currencyWindowText.text = Convert.ToString((Convert.ToInt16(currencyWindowText.text) + n));

            yield return new WaitForSeconds(0.01f);
        }
    }
}
