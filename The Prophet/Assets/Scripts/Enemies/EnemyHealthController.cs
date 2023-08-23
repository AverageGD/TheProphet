using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _deathTiming;
    [SerializeField] private float _currencyMinimum;
    [SerializeField] private float _currencyMaximum;

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
        OnDeath?.Invoke();

        animator.SetTrigger("Death");
        gameObject.layer = LayerMask.NameToLayer("Invincible");

        yield return new WaitForSeconds(_deathTiming / 2);

        CinemachineShake.instance.Shake(2, _deathTiming / 2);

        yield return new WaitForSeconds(_deathTiming / 2);


        PlayerCurrencyController.instance.AddCurrency((int)( PlayerCurrencyController.instance.currencyMultiplier * Random.Range(_currencyMinimum, _currencyMaximum)));

        while (spriteRenderer.color.a > 0)
        {
            Color tmp = spriteRenderer.color;
            tmp.a = spriteRenderer.color.a - Time.deltaTime;
            spriteRenderer.color = tmp;

            yield return null;
        }

        Destroy(gameObject);
    }
    
}
