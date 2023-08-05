using System.Collections;
using UnityEngine;

public class DeathMessage : MonoBehaviour
{
    public static DeathMessage instance;

    private Animator animator;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        animator = GetComponent<Animator>();

        gameObject.SetActive(false);
    }

    public void ShowMessage()
    {
        gameObject.SetActive(true);
    }

    private void HideMessage()
    {
        animator.SetTrigger("Ending");
    }

    public void OnAnyKeyPressed()
    {
        if (gameObject.activeSelf)
        {
            HideMessage();

            GameManager.instance.ReloadCurrentSceneInvoker(2.15f);

            this.enabled = false;
        }
            
    }

    
}
