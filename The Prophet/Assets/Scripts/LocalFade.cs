using System.Collections;
using UnityEngine;

public class LocalFade : MonoBehaviour
{
    public static LocalFade instance;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void LocalFadeInvoker() //Calls the private coroutie for fading
    {
        gameObject.SetActive(true);

        StartCoroutine(LocalFadeAction());
    }

    private IEnumerator LocalFadeAction() //Waits 2.16 seconds and disables Fade canvas object
    {
        yield return new WaitForSeconds(2.16f);

        gameObject.SetActive(false);
    }
}
