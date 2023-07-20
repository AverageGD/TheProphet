using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public float currencyMultiplier;

    private float health;

    private void Start()
    {
        health = _maxHealth;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
