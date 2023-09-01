using UnityEngine;

public class TheLoversProjectileController : MonoBehaviour
{
    public float radius = 4f;
    public float smoothTimeIdle = 0.25f;
    public float smoothTimeAttack = 0.05f;
    public float damage = 1f;
    public LayerMask enemyLayer;

    private Transform target;
    private Vector2 currentVelocity = Vector2.zero;

    private void Update()
    {
        float smoothTime;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        if (enemies.Length > 0)
        {
            target = enemies[0].transform;
            smoothTime = smoothTimeAttack;
        } else
        {
            if (CharacterController2D.instance != null)
                target = CharacterController2D.instance.transform;
            smoothTime = smoothTimeIdle;
        }

        if (target == null)
            return;

        transform.position = Vector2.SmoothDamp(transform.position, target.position, ref currentVelocity, smoothTime);

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealthController>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
