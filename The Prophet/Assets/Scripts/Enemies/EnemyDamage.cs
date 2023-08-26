using UnityEngine;

public class EnemyDamage : MonoBehaviour //Universal script for enemies' weapons logic
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private DamageType _damageType;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            Vector2 hitDirection = collision.transform.position - transform.position;
            hitDirection.Normalize();
            hitDirection = new Vector2(hitDirection.x, 0);

            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage, hitDirection, true);

            StatusEffectManager.instance.GivePlayerCorrespondingStatusEffect(_damageType);

            if (PlayerHealthController.instance.health <= 0)
            {
                transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                DeathScreen.instance.CreateSilhouette(_enemy.transform, _enemy.transform.GetComponent<SpriteRenderer>());
            }
        }
    }

    public enum DamageType
    {
        clear,
        poison,
    }
}
