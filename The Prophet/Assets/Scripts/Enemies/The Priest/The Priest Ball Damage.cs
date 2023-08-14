using System.Collections;
using UnityEngine;

public class ThePriestBallDamage : EnemyDamage
{
    [SerializeField] private float _speed;

    private Animator animator;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Player"))
        {
            CinemachineShake.instance.Shake(0.5f, 0.2f);

            animator.SetTrigger("Explosion");
            Destroy(gameObject, 0.1f);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DefaultDeath());
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, CharacterController2D.instance.transform.position, _speed * Time.deltaTime);
    }

    private IEnumerator DefaultDeath()
    {
        yield return new WaitForSeconds(1.85f);

        CinemachineShake.instance.Shake(0.5f, 0.2f);
        animator.SetTrigger("Explosion");
    }

}
