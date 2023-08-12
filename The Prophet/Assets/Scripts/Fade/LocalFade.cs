using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LocalFade : MonoBehaviour
{
    public static LocalFade instance;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;

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
        OnStart?.Invoke();

        yield return new WaitForSeconds(1.5f);

        OnEnd?.Invoke();

        yield return new WaitForSeconds(0.66f);

        gameObject.SetActive(false);

    }
}
