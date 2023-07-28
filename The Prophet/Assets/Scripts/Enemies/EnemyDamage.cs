using UnityEngine;

public class EnemyDamage : MonoBehaviour //Universal script for enemies's weapons logic
{
    [SerializeField] private float damage;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);

            if (collision.gameObject.GetComponent<PlayerHealthController>().health <= 0)
            {
                DeathScreen.instance.CreateSilhouette(transform.parent, transform.parent.GetComponent<SpriteRenderer>());
            }
        }
    }
}
