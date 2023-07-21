using System.Collections;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _attackDistance;

    public bool isAttacking = false;

    private bool isPlayerVeryNear;
    private bool canAttack = true;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isPlayerVeryNear = Physics2D.OverlapCircle(transform.position, _attackDistance, _playerLayer);

        if (isPlayerVeryNear && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1.5f);

        if (isPlayerVeryNear)
            _player.gameObject.GetComponent<PlayerHealthController>().TakeDamage(1);

        yield return new WaitForSeconds(4f);
        isAttacking = false;
        canAttack = true;

        print("Attacked");
    }
    
}
