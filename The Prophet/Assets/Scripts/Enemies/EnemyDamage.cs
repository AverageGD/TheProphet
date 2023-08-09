using UnityEngine;

public class EnemyDamage : MonoBehaviour //Universal script for enemies's weapons logic
{
    [SerializeField] private float damage;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private DamageType _damageType;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(damage);

            StatusEffectManager.instance.GivePlayerCorrespondingStatusEffect(_damageType);

            if (PlayerHealthController.instance.health <= 0)
            {
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
