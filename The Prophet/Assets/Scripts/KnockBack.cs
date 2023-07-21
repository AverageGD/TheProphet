using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 10f;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void KnockbackInvoker(Vector2 direction)
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}
