using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThePriestAttacks : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _visibilityDistance;
    [SerializeField] private GameObject _batPrefab;

    public bool isPlayerNear;
    public UnityEvent OnStart;
    public UnityEvent OnEnd;

    private Animator animator;
    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isPlayerNear = Physics2D.OverlapCircle(transform.position, _visibilityDistance, _playerLayer);

        if (isPlayerNear)
        {
            short direction = (short)Mathf.Sign(_player.position.x - transform.position.x);

            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (canAttack)
                StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        OnStart?.Invoke();

        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.4f);

        float numberOfBats = Random.Range(1, 4);

        print(numberOfBats);

        for (int i = 0; i < numberOfBats; i++)
        {
            GameObject batPrefabClone = Instantiate(_batPrefab);

            batPrefabClone.transform.position = _attackPoint.position;

            batPrefabClone.GetComponent<ThePriestBatDamage>().player = _player;

            Destroy(batPrefabClone, 2);

            yield return new WaitForSeconds(1.5f);
            print("Spawned");
        }

        yield return new WaitForSeconds(0.2f);

        animator.SetTrigger("AttackRecovery");

        yield return new WaitForSeconds(1.5f);

        OnEnd?.Invoke();
        canAttack = true;

    }
}
