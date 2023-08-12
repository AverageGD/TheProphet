using System.Collections;
using UnityEngine;

public class LeperAttacks : MonoBehaviour
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _swordPrefab;

    private Animator animator;
    private GameObject attackPoint;
    private bool isPlayerNear;
    private bool canAttack;
    private bool isAttacking;

    private void Start()
    {
        //setting up default values
        canAttack = true;
        isAttacking = false;

        animator = GetComponent<Animator>();
        attackPoint = transform.Find("AttackPoint").gameObject;
        attackPoint.SetActive(false);
    }

    private void Update()
    {
        isPlayerNear = Physics2D.OverlapCircle(transform.position, _attackDistance, _playerLayer); //checks if player is near enought to strike him

        if (canAttack && isPlayerNear) //if player is very near and enemy can attack, then he starts the attack
        {
            StartCoroutine(RangeAttack());
        }
    }

    private IEnumerator RangeAttack()
    {
        animator.SetTrigger("RangeAttackOpen");

        short direction = (short)Mathf.Sign(_player.position.x - transform.position.x);

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        canAttack = false;
        isAttacking = true;

        yield return new WaitForSeconds(0.45f);

        GameObject swordClone = Instantiate(_swordPrefab, transform);

        swordClone.transform.position = attackPoint.transform.position;

        while (swordClone != null)
        {
            yield return null;
        }

        animator.SetTrigger("RangeAttackClose");

        yield return new WaitForSeconds(0.3f);

        isAttacking = false;

        yield return new WaitForSeconds(_cooldownTime);

        canAttack = true;
    }
}
