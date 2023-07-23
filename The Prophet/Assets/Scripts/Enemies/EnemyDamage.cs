using UnityEngine;

public class EnemyDamage : MonoBehaviour //Universal script for enemies's weapons logic
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);
        }
    }
}
