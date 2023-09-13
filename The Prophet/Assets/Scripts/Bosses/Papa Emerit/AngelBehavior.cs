using System.Collections;
using UnityEngine;

public class AngelBehavior : MonoBehaviour
{
    [SerializeField] private Transform _movementTarget;
    [SerializeField] private GameObject _shockWave;

    private Vector3 startPosition;
    private Vector2 movementVelocity;
    private Animator animator;

    private void OnEnable()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();

        StartCoroutine(AngelBehaviorAction());
    }

    private IEnumerator AngelBehaviorAction()
    {
        while (transform.position.y > _movementTarget.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, _movementTarget.position, Time.deltaTime);

            yield return null;
        }

        print("reached");

        animator.SetTrigger("Attack");
        GameObject shockWaveClone = Instantiate(_shockWave);
        shockWaveClone.transform.position = transform.Find("ShockWavePoint").position;
        Destroy(shockWaveClone, 1f);

        yield return new WaitForSeconds(0.7f);

        while (transform.position.y < startPosition.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, Time.deltaTime);

            yield return null;
        }

        print("reverse reached");

        transform.parent.gameObject.SetActive(false);
    }
}
