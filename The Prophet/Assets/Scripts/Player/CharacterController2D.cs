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
    private float fallSpeedYDampingChangeTreshold;

    public float dashPower;
    public short upgradeLevel;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        direction = 1;
        _spriteRenderer.flipX = false;

        fallSpeedYDampingChangeTreshold = CameraManager.instance._fallSpeedYDampingChangeTreshold;
    }

    private void Update()
    {
        if (IsWallNear() && !IsGrounded()) //if the player is near to wall but is not grounded, that means he is wallsliding
            rigidBody.gravityScale = 1f;
        else if (!isDashing) //if the player is not dashing(because there is conflict between dash and this part), then we set gravity as normal
            rigidBody.gravityScale = 4f;


        if (IsWallNear()) //checks if player is near to the wall, to turn on the collider in case of dashing near the walls
        {
            print("wall is near!");
            _playerCollider.enabled = true;
        }


        if (isDashing || isAttacking) return; //when player dashes or attacks he should not do anything else

        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (horizontalAxis > 0)
        {
            direction = 1;
            _spriteRenderer.flipX = false;
        }
        else if (horizontalAxis < 0)
        {
            direction = -1;
            _spriteRenderer.flipX = true;
        }



        _attackPoint.localPosition = new Vector2(direction * 0.63f, _attackPoint.localPosition.y); //determines the local position of attack point by depending on direction

        _wallChecker.localPosition = new Vector2(direction * 0.48f, _wallChecker.localPosition.y); //determines the local position of wallchecker point by depending on direction

        if (rigidBody.velocity.y < fallSpeedYDampingChangeTreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rigidBody.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
    }

    public void Jump()
    {
        if (IsGrounded() || (upgradeLevel >= 2 && IsWallNear() && Time.time - lastJumpTime > 0.35f)) //checks if player is grounded, or is near to the wall to jump
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 18);
            lastJumpTime = Time.time;
        }
    }

    public void AttackInvoker() //is called by new input manager
    {
        if (canAttack)
        {
            StartCoroutine(Attack()); //starts atack coroutine
        }
    }

    public void DashInvoker() //is called by new input manager
    {
        if (horizontalAxis != 0 && canDash)
        {
            StartCoroutine(Dash()); //starts dash coroutine
        }
    }

    private IEnumerator Dash()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible"); // Changes player's layer to avoid contact with enemies
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
        if (!Input.GetButtonDown("Jump") && rigidBody.velocity.y > 0f) return false;
        else return Physics2D.OverlapCircle(_wallChecker.position, 0.2f, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_groundChecker.position, 0.3f);
        Gizmos.DrawWireSphere(_wallChecker.position, 0.2f);
        Gizmos.DrawWireSphere(_attackPoint.position, 0.2f);
    }

}
