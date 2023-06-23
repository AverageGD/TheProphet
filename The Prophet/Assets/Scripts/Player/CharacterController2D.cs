using System.Collections;
using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private float horizontalAxis;
    private Rigidbody2D rigidBody;
    private bool canDash = true;
    private bool canAttack = true;
    private bool isDashing;
    private bool isAttacking;
    private short direction;
    private short currentNumberOfAttacks = 0;
    private short damage = 1;
    private float lastJumpTime;

    public float dashPower;
    public short upgradeLevel;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        direction = 1;
        _spriteRenderer.flipX = false;
    }

    private void Update()
    {
        if (IsWallNear())
        {
            print("wall is near!");
            _playerCollider.enabled = true;
        }

        if (isDashing || isAttacking) return;

        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (horizontalAxis > 0)

            direction = 1;

        else if (horizontalAxis < 0)

            direction = -1;


        _attackPoint.localPosition = new Vector2(direction * 0.63f, _attackPoint.localPosition.y);

        _wallChecker.localPosition = new Vector2(direction * 0.36f, _wallChecker.localPosition.y);
    }

    public void Jump()
    {
        if (IsGrounded() || upgradeLevel >= 2 && IsWallNear() && Time.time - lastJumpTime > 0.3f)
        {
            lastJumpTime = Time.time;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 8f);
        }
    }

    public void AttackInvoker()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    public void DashInvoker()
    {
        if (horizontalAxis != 0 && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        gameObject.layer = LayerMask.NameToLayer("Dash"); // Changes player's layer to avoid contact with enemies
        canDash = false;
        isDashing = true;
        float origGravity = rigidBody.gravityScale; // keeps default gravity scale

        if (upgradeLevel >= 5)
        {
            //strikes damage to enemies
        }

        rigidBody.gravityScale = 0;
        rigidBody.velocity = Vector2.zero;
        rigidBody.velocity = new Vector2(direction * dashPower, 0f); // adds speed with player's current direction
        _playerCollider.enabled = false;

        yield return new WaitForSeconds(0.2f);

        rigidBody.gravityScale = origGravity; //returns default gravity scale
        gameObject.layer = LayerMask.NameToLayer("Player"); //returns player's default layer
        _playerCollider.enabled = true;
        isDashing = false;

        yield return new WaitForSeconds(1.5f);
        canDash = true;
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;

        print("lock");

        short maxNumberOfAttacks = (short)(upgradeLevel >= 1 ? 4 : 3);
        currentNumberOfAttacks++;

        if (currentNumberOfAttacks > maxNumberOfAttacks)
        {
            currentNumberOfAttacks = 1;
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, 0.2f, _enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy != null)
            {
                // When enemies are created here will be calling function for taking damage
            }
        }

        yield return new WaitForSeconds(0.3f);

        print("open");

        canAttack = true;
        isAttacking = false;
    }
    private void FixedUpdate()
    {
        //basic movement logic

        float targetSpeed = horizontalAxis * _speed; // The speed we need to reach

        float speedDif = targetSpeed - rigidBody.velocity.x; // The difference between targetSpeed and current speed 

        float accelRate = (Mathf.Abs(speedDif) > 0.01f) ? _acceleration : _decceleration; // if player stops/starts movement

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velocityPower) * Mathf.Sign(speedDif); // final value of movement speed

        rigidBody.AddForce(movement * Vector2.right); // giving speed force to player
    }

    //Checks if player is grounded
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, 0.3f, _groundLayer);
    }

    private bool IsWallNear()
    {
        return Physics2D.OverlapCircle(_wallChecker.position, 0.2f, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, 0.3f);
        Gizmos.DrawWireSphere(_wallChecker.position, 0.2f);
        Gizmos.DrawWireSphere(_attackPoint.position, 0.2f);
    }

}
