using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThePriestAttacks : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _visibilityDistance;
    [SerializeField] private float _meleeAttackDistance;
    [SerializeField] private GameObject _ballPrefab;

    private GameObject meleeAttackPoint;
    private bool isPlayerVeryNear;
    private bool isPlayerNear;
    private Animator animator;
    private bool canAttack = true;

    public UnityEvent OnStart;
    public UnityEvent OnEnd;


    private void Start()
    {
        meleeAttackPoint = transform.Find("MeleeAttackPoint").gameObject;
        animator = GetComponent<Animator>();

        meleeAttackPoint.SetActive(false);
    }

    private void Update()
    {
        isPlayerNear = Physics2D.OverlapCircle(transform.position, _visibilityDistance, _playerLayer);
        isPlayerVeryNear = Physics2D.OverlapCircle(transform.position, _meleeAttackDistance, _playerLayer);

        if (isPlayerNear)
            animator.SetBool("IsWalking", false);

        if (canAttack)
        {
            if (isPlayerVeryNear)
                StartCoroutine(MeleeAttack());
            else if (isPlayerNear)
                StartCoroutine(RangeAttack());
        }
    }

    private IEnumerator RangeAttack()
    {
        canAttack = false;
        OnStart?.Invoke();

        animator.SetTrigger("RangeAttack");

        yield return new WaitForSeconds(0.4f);

        float numberOfBalls = Random.Range(1, 4);

        for (int i = 0; i < numberOfBalls; i++)
        {
            short direction = 1;

            if (CharacterController2D.instance != null)
            direction = (short)Mathf.Sign(CharacterController2D.instance.transform.position.x - transform.position.x);

            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            GameObject ballPrefabClone = Instantiate(_ballPrefab);

            ballPrefabClone.transform.position = _attackPoint.position;


            Destroy(ballPrefabClone, 2);

            yield return new WaitForSeconds(1.5f);
            print("Spawned");
        }

        yield return new WaitForSeconds(0.2f);

        animator.SetTrigger("AttackRecovery");

        yield return new WaitForSeconds(2f);

        if (GetComponent<EnemyHealthController>().health > 0)
            OnEnd?.Invoke();

        canAttack = true;
    }

    private IEnumerator MeleeAttack()
    {
        canAttack = false;
        OnStart?.Invoke();

        short direction = (short)Mathf.Sign(CharacterController2D.instance.transform.position.x - transform.position.x);

        if (direction == 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        animator.SetTrigger("MeleeAttack");

        yield return new WaitForSeconds(0.6f);

        meleeAttackPoint.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        meleeAttackPoint.SetActive(false);

        yield return new WaitForSeconds(2f);

        canAttack = true;
        OnEnd?.Invoke();
    }
}
