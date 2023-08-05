using System.Collections;
using UnityEngine;

public class GlobalFade : MonoBehaviour
{
    public static GlobalFade instance;

    private Animator animator;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        animator = GetComponent<Animator>();

        StartCoroutine(FadeOut());
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);

        animator.SetTrigger("FadeIn");
    }

    private IEnumerator FadeOut()
    {
        gameObject.SetActive(true);

        yield return new WaitForSeconds(1.16f);

        gameObject.SetActive(false);
    }
}
