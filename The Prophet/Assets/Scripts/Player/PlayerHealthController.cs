using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    private float health;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        Flash.instance.FlashSpriteInvoker(spriteRenderer);
        GameManager.instance.InvincibilityInvoker(gameObject, 2.5f, true);
        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.08f);
        Destroy(gameObject);
    }
}
