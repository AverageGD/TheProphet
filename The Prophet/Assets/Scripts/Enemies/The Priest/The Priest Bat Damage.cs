using UnityEngine;

public class ThePriestBatDamage : EnemyDamage
{
    [SerializeField] private float _speed;

    public Transform player;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, _speed * Time.deltaTime);
    }

}
