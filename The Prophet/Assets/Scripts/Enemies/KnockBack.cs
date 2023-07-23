using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    public static Knockback instance;

    public float knockbackForce;
    public float knockbackDelay;
    public UnityEvent OnBegin;
    public UnityEvent OnDone;

    private Rigidbody2D rigidBody;

    private void Start()
    {
        if (instance == null)
            instance = this;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void KnockbackInvoker(Transform sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 knockbackDirection = (transform.position - sender.position).normalized;
        rigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(knockbackDelay);

        rigidBody.velocity = Vector2.zero;
        OnDone?.Invoke();
    }
}
