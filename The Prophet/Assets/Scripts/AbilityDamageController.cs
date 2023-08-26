using UnityEngine;

public class AbilityDamageController : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthController>().TakeDamage(damage);
            print("Touched Enemy");
        }
    }
}
