using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.ParticleSystem;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _velocityPower;
    [SerializeField] private Transform _groundCheckerTransform;
    [SerializeField] private Collider2D _playerCollider;

    private float horizontalAxis;
    private Rigidbody2D rigidBody;
    private bool canDash = true;
    private bool isDashing;
    private short direction;

    public float dashPower;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing) return;

        horizontalAxis = Input.GetAxisRaw("Horizontal");

        direction = (short)(horizontalAxis >= 0 ? 1 : -1);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 18f);
        }

        if (rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
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
        return Physics2D.OverlapCircle(_groundCheckerTransform.position, 0.3f, _groundLayer);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_groundCheckerTransform.position, 0.3f);
    }
}
