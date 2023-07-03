using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade instance;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void FadeInvoker() //Calls the private coroutie for fading
    {
        gameObject.SetActive(true);

        StartCoroutine(FadeLogic());
    }

    private IEnumerator FadeLogic() //Waits 2.16 seconds and disables Fade canvas object
    {
        yield return new WaitForSeconds(2.16f);

        gameObject.SetActive(false);
    }
}
