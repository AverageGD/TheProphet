using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _deathTiming;

    public float currencyMultiplier;
    public float health;
    public UnityEvent OnDeath;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = _maxHealth;

        animator.SetFloat("Health", _maxHealth);
    }
    public void TakeDamage(float damage)
    {
        Flash.instance.FlashSpriteInvoker(spriteRenderer);

        health -= damage;

        animator.SetFloat("Health", health);
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Death");
        gameObject.layer = LayerMask.NameToLayer("Invincible");

        yield return new WaitForSeconds(_deathTiming / 2);

        CinemachineShake.instance.Shake(2, _deathTiming / 2);

        yield return new WaitForSeconds(_deathTiming / 2);


        OnDeath?.Invoke();
        
        while (spriteRenderer.color.a > 0)
        {
            spriteRenderer.color = spriteRenderer.color.WithAlpha(spriteRenderer.color.a - Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
    
}
