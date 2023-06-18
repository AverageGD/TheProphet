using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;
    [SerializeField] private float _velocityPower;

    private float fallSpeed = 0.5f;
    private float horizontalAxis;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 18f);
        }

        if (Input.GetButtonDown("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * fallSpeed);
        }
    }

    private void FixedUpdate()
    {

        float targetSpeed = horizontalAxis * _speed;

        float speedDif = targetSpeed - rigidBody.velocity.x;

        float accelRate = (Mathf.Abs(speedDif) > 0.01f) ? _acceleration : _decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velocityPower) * Mathf.Sign(speedDif);

        rigidBody.AddForce(movement * Vector2.right);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, 0.5f, _groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
