using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private SpriteRenderer spriteRenderer;

    public float health;
    public UnityEvent OnDeath;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        VibrationController.instance.StartVibration(0.3f, 0.3f, 0.5f);
        GameManager.instance.InvincibilityInvoker(gameObject, 1.5f, true);

        Flash.instance.FlashSpriteInvoker(spriteRenderer);
        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        OnDeath?.Invoke();

        DeathScreen.instance.CreateSilhouette(transform, spriteRenderer);

        yield return new WaitForSeconds(0.08f);

        gameObject.SetActive(false);
    }
}
