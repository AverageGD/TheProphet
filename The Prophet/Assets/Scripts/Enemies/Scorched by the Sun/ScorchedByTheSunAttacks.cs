using System.Collections;
using UnityEngine;

public class ScorchedByTheSunAttacks : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _attackDistance;

    public bool isAttacking = false;

    private bool isPlayerVeryNear;
    private bool canAttack = true;
    private Animator animator;
    private GameObject attackPoint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        attackPoint = transform.Find("AttackPoint").gameObject;
        attackPoint.SetActive(false);
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

        attackPoint.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        attackPoint.SetActive(false);

        yield return new WaitForSeconds(4.5f);

        isAttacking = false;
        canAttack = true;

        print("Attacked");
    }
    
}
