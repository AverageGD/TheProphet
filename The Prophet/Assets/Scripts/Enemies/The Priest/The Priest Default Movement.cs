using System.Collections;
using UnityEngine;

public class ThePriestDefaultMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _safePositionChecker;
    [SerializeField] private float _safePositionCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _visibilityDistance;
    [SerializeField] private float _firstPoint;
    [SerializeField] private float _secondPoint;
    [SerializeField] private float _patrollingTime;

    public bool isPlayerNear;

    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool isFirstPointTargeted = false;
    

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        Vector3 target = Vector3.zero;

        if (isFirstPointTargeted)
            target = new Vector2(_firstPoint, transform.position.y);
        else 
            target = new Vector2(_secondPoint, transform.position.y);

        Vector3 direction = target - transform.position;

        if (direction.magnitude < 0.5f)
            StartCoroutine(Patrolling());

        direction.Normalize();

        rigidBody.velocity = direction * _speed;
    }

    private IEnumerator Patrolling()
    {

        yield return new WaitForSeconds(_patrollingTime);

        isFirstPointTargeted = !isFirstPointTargeted;
    }
}
