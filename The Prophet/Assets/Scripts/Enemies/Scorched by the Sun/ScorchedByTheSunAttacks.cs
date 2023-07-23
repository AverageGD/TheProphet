using System.Collections;
using UnityEngine;

public class ScorchedByTheSunAttacks : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _attackDistance;

    private bool canAttack = true;
    public bool isAttacking = false;

    private bool isPlayerVeryNear;
    private float health;
    private Animator animator;
    private GameObject attackPoint;

    private void Start()
    {
        //setting up default values
        animator = GetComponent<Animator>();
        attackPoint = transform.Find("AttackPoint").gameObject;
        attackPoint.SetActive(false);
    }

    private void Update()
    {
        isPlayerVeryNear = Physics2D.OverlapCircle(transform.position, _attackDistance, _playerLayer); //checks if player is near enought to strike him

        if (canAttack && isPlayerVeryNear) //if player is very near and enemy can attack, then he starts the attack
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        animator.SetTrigger("Attack");

        short direction = (short)Mathf.Sign(_player.position.x - transform.position.x);

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        yield return new WaitForSeconds(1.5f);

        attackPoint.SetActive(true);

        VibrationController.instance.StartVibration(0.5f, 0.5f, 0.5f);

        CinemachineShake.instance.Shake(2, 0.5f);

        yield return new WaitForSeconds(0.5f);

        attackPoint.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
        canAttack = true;

        print("Attacked");
    }
    
}
