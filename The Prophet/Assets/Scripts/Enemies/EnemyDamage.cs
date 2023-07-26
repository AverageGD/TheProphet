using UnityEngine;

public class EnemyDamage : MonoBehaviour //Universal script for enemies's weapons logic
{
    [SerializeField] private float damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);
        }
    }
}
