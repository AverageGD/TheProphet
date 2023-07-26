using UnityEngine;

public class ThePriestBatDamage : EnemyDamage
{
    [SerializeField] private float _speed;


    public Transform player;
    public short Direction { private get; set; }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * Direction * _speed * Time.deltaTime);
    }

}
