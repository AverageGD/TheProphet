using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterController2D : MonoBehaviour
{
    public static CharacterController2D instance;

    [Header("Movement")]

    [SerializeField] private float _speed;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private Transform _safePositionChecker;
    [SerializeField] private float _safePositionCheckDistance;
    [SerializeField] private float _ladderClimbingSpeed;
    [SerializeField] private float _dashPower;

    [Header("Attack")]

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackDistance;

    [Header("Interaction")]

    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _interactionRadius;

    [Header("Animation")]

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerGhost;

    [Header("Audio")]

    [SerializeField] private AudioContainer _footstepAudioContainer;
    [SerializeField] private AudioContainer _jumpAudioContainer;

    private float verticalAxis;
    private bool isLadder;
    private bool isClimbingLadder;
    private float horizontalAxis;
    private Rigidbody2D rigidBody;
    private bool canDash = true;
    private bool canAttack = true;
    private bool isDashing;
    private bool isAttacking;
    private short direction;
    private short currentNumberOfAttacks = 0;
    private float lastAttackTime;
    private short damage = 1;
    private float lastJumpTime;
    private float fallSpeedYDampingChangeTreshold;
    private Transform currentLadder;
    private float lastPlayerGhostSpawnTime;
    private GameObject dashDamage;
    private AudioSource audioSource;

    private float stepSoundElapsedTime = 0f;
    private int stepSoundClipIndex;

    [Header("Other")]

    public Vector2 lastSafePosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody2D>();
    
        direction = 1;
        _spriteRenderer.flipX = false;

        fallSpeedYDampingChangeTreshold = CameraManager.instance._fallSpeedYDampingChangeTreshold;

        dashDamage = transform.Find("DashDamage").gameObject;
        dashDamage.GetComponent<AbilityDamageController>().damage = (float)damage / 2;
    }

    private void Update()
    {
        _animator.SetBool("IsGrounded", GameManager.instance.IsGrounded(_groundChecker, _groundCheckDistance));
        _animator.SetBool("IsClimbingLadder", isClimbingLadder);
        _animator.SetBool("IsWallSliding", IsWallNear());

        if (GameManager.instance.IsGrounded(_groundChecker, _groundCheckDistance) && stepSoundElapsedTime > 0.4f
            && horizontalAxis != 0)
        {
            stepSoundElapsedTime = 0;
            audioSource.clip = _footstepAudioContainer.audioClips[stepSoundClipIndex];
            audioSource.volume = _footstepAudioContainer.volume; 
            audioSource.pitch = _footstepAudioContainer.pitch;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            stepSoundClipIndex++;
        }

        stepSoundElapsedTime += Time.deltaTime;
        stepSoundClipIndex %= _footstepAudioContainer.audioClips.Count;

        if (IsWallNear() && !GameManager.instance.IsGrounded(_groundChecker, _groundCheckDistance)) //if the player is near to wall but is not grounded, that means he is wallsliding
        {
            rigidBody.gravityScale = 0f;
            rigidBody.gravityScale = 1f;

        } else if (!isDashing) //if the player is not dashing(because there is conflict between dash and this part), then we set gravity as normal

            rigidBody.gravityScale = 4f;

        //getting values from axises
        verticalAxis = Input.GetAxisRaw("Vertical");
        horizontalAxis = Input.GetAxisRaw("Horizontal");


        if (Mathf.Abs(verticalAxis) > Mathf.Abs(horizontalAxis) && isLadder && !isDashing) //if the player is near to the ladder and vertical axis isn't equal to 0, then he starts to climb
        {
            isClimbingLadder = true;
            _animator.SetBool("IsMovingVertically", true);
        } else
        {
            _animator.SetBool("IsMovingVertically", false);
        }

        if (Time.time - lastAttackTime > 1) currentNumberOfAttacks = 0;

        if (horizontalAxis > 0)
        {
            direction = 1;
        } else if (horizontalAxis < 0)
        {
            direction = -1;
        }

        if (isClimbingLadder || Knockback.instance.IsBeingKnockedBack) return; //When player uses a ladder he should not do anything else

        TrySpawnPlayerGhost();

        if (isDashing || isAttacking) return; //when player dashes or attacks he should not do anything else

        if (horizontalAxis > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontalAxis < 0)
        {
            _spriteRenderer.flipX = true;
        }

        _attackPoint.localPosition = new Vector2(direction * 2.4f, _attackPoint.localPosition.y); //determines the local position of attack point by depending on direction

        _wallChecker.localPosition = new Vector2(direction * 0.48f, _wallChecker.localPosition.y); //determines the local position of wallChecker point by depending on direction

        _safePositionChecker.localPosition = new Vector2(direction * 2f, _safePositionChecker.localPosition.y); //determines the local position of safePositionChecker point by depending on direction


        if (rigidBody.linearVelocity.y < fallSpeedYDampingChangeTreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rigidBody.linearVelocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;

            CameraManager.instance.LerpYDamping(false);
        }
    }

    private void FixedUpdate()
    {
        //movement on ladder logic

        #region Movement On Ladder

        float aligningToCenterOfLadderVelocity = 0;

        if (isClimbingLadder)
        {
            // aligns player to the center of the current ladder

            transform.position = new Vector2(Mathf.SmoothDamp(transform.position.x, currentLadder.position.x, ref aligningToCenterOfLadderVelocity, 0.05f), transform.position.y); 

            rigidBody.gravityScale = 0f; //turning gravity scale on 0 for climbing

            rigidBody.linearVelocity = new Vector2(0, verticalAxis * _ladderClimbingSpeed);

            return;
        }
        else if (!isDashing && !IsWallNear())
        {
            rigidBody.gravityScale = 4f;
        }
        #endregion

        //basic movement logic

        #region Basic Movement

        LastSafePositionDeterminer();


        float targetSpeed = horizontalAxis * _speed; // The speed we need to reach

        float speedDif = targetSpeed - rigidBody.linearVelocity.x; // The difference between targetSpeed and current speed 

        float accelRate = (Mathf.Abs(speedDif) > 0.01f) ? _acceleration : _decceleration; // if player stops/starts movement

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velocityPower) * Mathf.Sign(speedDif); // final value of movement speed

        rigidBody.AddForce(movement * Vector2.right); // giving speed force to player

        if (horizontalAxis != 0)

            _animator.SetBool("IsRunning", true);
        else

            _animator.SetBool("IsRunning", false);

        #endregion
    }

    #region AttackLogics
    public void AttackInvoker() //is called by new input manager
    {
        if (canAttack && !isClimbingLadder && gameObject.activeSelf && !(IsWallNear() && !GameManager.instance.IsGrounded(_groundChecker, _groundCheckDistance)) && !PlayerHealthController.instance.isHealing)
        {
            isClimbingLadder = false;
            StartCoroutine(Attack()); //starts atack coroutine
        }
    }
    private IEnumerator Attack()
    {
        canAttack = false;
        isAttacking = true;
        lastAttackTime = Time.time;

        GameManager.instance.FreezeRigidbodyInvoker(0.2f, rigidBody);


        short maxNumberOfAttacks = (short)(UpgradeSystemManager.instance.CanUseAbility("The Fool") ? 4 : 3);

        currentNumberOfAttacks++;

        if (currentNumberOfAttacks > maxNumberOfAttacks)
        {
            currentNumberOfAttacks = 1;
        }

        _animator.SetTrigger("Attack" + currentNumberOfAttacks);

        Collider2D[] enemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackDistance, _enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy != null)
            {
                VibrationController.instance.StartVibration(0.2f, 0.2f, 0.3f);

                CinemachineShake.instance.Shake(3, 0.3f);
                if (currentNumberOfAttacks == 4)
                {
                        enemy.gameObject.GetComponent<EnemyHealthController>().TakeDamage(2 * damage);
                } else
                {
                        enemy.gameObject.GetComponent<EnemyHealthController>().TakeDamage(damage);
                }
                PlayerManaController.instance.ReceiveMana(0.15f);
            }
        }

        if (currentNumberOfAttacks == 4)
        {
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.25f);

        isAttacking = false;

        yield return new WaitForSeconds(0.06f);

        canAttack = true;
    }
    #endregion 

    #region DashLogics
    public void DashInvoker() //is called by new input manager
    {
        if (horizontalAxis != 0 && canDash && !PlayerHealthController.instance.isHealing)
        {
            StartCoroutine(Dash()); //starts dash coroutine
        }
    }

    private IEnumerator Dash()
    {
        _animator.SetTrigger("Dash");

        gameObject.layer = LayerMask.NameToLayer("Invincible"); // Changes player's layer to avoid contact with enemies
        canDash = false;
        isDashing = true;


        if (UpgradeSystemManager.instance.CanUseAbility("The Hermit"))
            dashDamage.SetActive(true);

        isClimbingLadder = false;
        currentLadder = null;
        isLadder = false;

        if (horizontalAxis > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontalAxis < 0)
        {
            _spriteRenderer.flipX = true;
        }

        float origGravity = rigidBody.gravityScale; // keeps default gravity scale

        rigidBody.gravityScale = 0;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.linearVelocity = new Vector2(direction * _dashPower, 0f); // adds speed with player's current direction
        yield return new WaitForSeconds(0.2f);

        rigidBody.gravityScale = origGravity; //returns default gravity scale
        gameObject.layer = LayerMask.NameToLayer("Player"); //returns player's default layer

        dashDamage.SetActive(false);

        isDashing = false;

        yield return new WaitForSeconds(0.5f);
        canDash = true;
    }

    private void TrySpawnPlayerGhost() //Checks if it real to spawn the clone of player's ghost
    {
        if (isDashing && Time.time - lastPlayerGhostSpawnTime > 0.04f)
        {
            lastPlayerGhostSpawnTime = Time.time;
            GameObject playerGhostClone = Instantiate(_playerGhost, transform.position, new Quaternion());
            playerGhostClone.GetComponent<SpriteRenderer>().flipX = _spriteRenderer.flipX;
            Destroy(playerGhostClone, 0.4f);
        }
    }

    #endregion

    public void InteractionInvoker() // Calls when player Interacts with enviroment
    {
        if (PlayerHealthController.instance.isHealing)
            return;

        Collider2D[] interactionalObjects = Physics2D.OverlapCircleAll(transform.position, _interactionRadius, _interactableLayer); // Finds each interactable object in area
        if (interactionalObjects.Length > 0)
            interactionalObjects[0].gameObject.GetComponent<Interactable>().Interact(); //Calls an override interaction function from the first interactable object
    }

    public void ResetVelocity()
    {
        rigidBody.linearVelocity = Vector3.zero;

    }

    public void Jump()
    {
        if (isAttacking)
            return;

        if (GameManager.instance.IsGrounded(_groundChecker, _groundCheckDistance) || ((UpgradeSystemManager.instance.CanUseAbility("The Hanged Man") && IsWallNear() || isClimbingLadder) && Time.time - lastJumpTime > 0.8f)
            && !PlayerHealthController.instance.isHealing) //checks if player is grounded, or is near to the wall to jump
        {
            audioSource.volume = _jumpAudioContainer.volume;
            audioSource.pitch = _jumpAudioContainer.pitch;

            audioSource.clip = _jumpAudioContainer.audioClips[0];
            audioSource.Play();

            _animator.SetTrigger("Jump");
            isClimbingLadder = false;
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, 18);
            lastJumpTime = Time.time;
        }
    }

    //Checks if player is near to the wall
    private bool IsWallNear()
    {
        if (!Input.GetButtonDown("Jump") && rigidBody.linearVelocity.y > 0f) return false;
        else return Physics2D.OverlapCircle(_wallChecker.position, _wallCheckDistance, _groundLayer);
    }

    //Checks if the player's current position is safe to keep in the lastSafePosition
    private void LastSafePositionDeterminer()
    {
        if (GameManager.instance.IsGrounded(_groundChecker, _groundCheckDistance) && Physics2D.OverlapCircle(_safePositionChecker.position, _safePositionCheckDistance, _groundLayer) && rigidBody.linearVelocity.y == 0)
        {
            lastSafePosition = transform.position;
        }
    }

    #region LadderDetectingLogics
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            currentLadder = collision.gameObject.GetComponent<Transform>();
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            if (rigidBody.linearVelocity.y > 0)
                Jump();

            currentLadder = null;
            isClimbingLadder = false;
            _animator.SetBool("IsMovingVertically", false);
            isLadder = false;
        }
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_groundChecker.position, _groundCheckDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_wallChecker.position, _wallCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_safePositionChecker.position, _safePositionCheckDistance);
    }

}
