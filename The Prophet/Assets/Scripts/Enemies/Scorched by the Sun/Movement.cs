using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _safePositionChecker;
    [SerializeField] private float _safePositionCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _visibilityDistance;

    private bool isPlayerNear;
    private bool isPlayerVeryNear;
    private Animator animator;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isPlayerNear = Physics2D.OverlapCircle(transform.position, _visibilityDistance, _playerLayer);
        isPlayerVeryNear = Physics2D.OverlapCircle(transform.position, _attackDistance, _playerLayer);

        animator.SetBool("IsPlayerNear", isPlayerNear && IsSafeGround());

        bool isAttacking = GetComponent<Attacks>().isAttacking; 

        if (isPlayerNear && !isPlayerVeryNear && !isAttacking)
        {
            short direction = (short)Mathf.Sign(_player.position.x - transform.position.x);

            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (IsSafeGround())
                rigidBody.velocity = Vector2.right * _speed * direction;
        }

        if (isPlayerVeryNear)
        {
            short direction = (short)Mathf.Sign(_player.position.x - transform.position.x);

            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private bool IsSafeGround()
    {
        return Physics2D.OverlapCircle(_safePositionChecker.position, _safePositionCheckDistance, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_safePositionChecker.position, _safePositionCheckDistance);
    }
}
