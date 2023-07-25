using UnityEngine;

public class ScorchedByTheSunMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _safePositionChecker;
    [SerializeField] private float _safePositionCheckDistance;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _speed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _visibilityDistance;

    public bool isPlayerNear;
    public bool isPlayerVeryNear;

    private Animator animator;
    private Rigidbody2D rigidBody;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isPlayerNear = Physics2D.OverlapCircle(transform.position, _visibilityDistance, _playerLayer); //Checks if player is near to approach him

        animator.SetBool("IsPlayerNear", isPlayerNear && IsSafeGround());

        bool isAttacking = GetComponent<ScorchedByTheSunAttacks>().isAttacking;

        if (isPlayerNear && !isAttacking) //If player is not attacking and player is near
        {
            short direction = (short)Mathf.Sign(_player.position.x - transform.position.x); //determines the direction of movement depending on the player's position

            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            } else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (IsSafeGround()) //if there is safe ground he moves
                rigidBody.velocity = Vector2.right * _speed * direction;
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
