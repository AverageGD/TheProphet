using System.Collections;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public static Knockback instance;

    private Rigidbody2D rigidbody;
    private Coroutine knockbackCoroutine;

    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10;
    public float constForce = 5f;
    public float inputForce = 7.5f;

    public bool IsBeingKnockedBack { get; private set; }

    private void Start()
    {
        instance = this;

        rigidbody = GetComponent<Rigidbody2D>();

    }

    private IEnumerator KnockbackAction(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        IsBeingKnockedBack = true;

        Vector2 _hitForce;
        Vector2 _constantForce;
        Vector2 _knockbackForce;
        Vector2 _combinedForce;

        _hitForce = hitDirection * hitDirectionForce;
        _constantForce = constantForceDirection * constForce;

        float _elapsedTime = 0f;

        while (_elapsedTime < knockbackTime)
        {
            _elapsedTime += Time.fixedDeltaTime;

            _knockbackForce = _hitForce + _constantForce;

            if (inputDirection != 0)
            {
                _combinedForce = _knockbackForce + new Vector2(inputDirection, 0);
            } else
            {
                _combinedForce = _knockbackForce;
            }

            rigidbody.velocity = _combinedForce;

            yield return new WaitForFixedUpdate();

        }

        IsBeingKnockedBack = false;
    }

    public void KnockbackInvoker(Vector2 hitDirection, Vector2 constantForceDirection, float inputDirection)
    {
        knockbackCoroutine = StartCoroutine(KnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Vector2 hitDirection = collision.transform.position - transform.position;
            hitDirection.Normalize();
            

            KnockbackInvoker(-2 * hitDirection, Vector2.zero, Input.GetAxisRaw("Horizontal"));
        }
    }
}
