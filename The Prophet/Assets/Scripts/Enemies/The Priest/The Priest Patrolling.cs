using System.Collections;
using UnityEngine;

public class ThePriestPatrolling : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _patrollingTime;

    public Vector3[] points;
    public int current;

    private float currentRotation = 0;
    private bool canMove = true;
    private bool isPatrolling = false;
    private Animator animator;
    private void Start()
    {
        current = 0;

        animator = GetComponent<Animator>();
    }

    
    private void Update()
    {
        if (transform.position != points[current] && canMove)
        {
            animator.SetBool("IsWalking", true);

            transform.position = Vector2.MoveTowards(transform.position, points[current], _speed * Time.deltaTime);
        }
        else if (!isPatrolling)
        {
            animator.SetBool("IsWalking", false);

            StartCoroutine(Patrolling());
            
        }
    }

    private IEnumerator Patrolling()
    {

        canMove = false;
        isPatrolling = true;

        yield return new WaitForSeconds(_patrollingTime);

        isPatrolling = false;
        canMove = true;

        transform.rotation = Quaternion.Euler(0, currentRotation + 180, 0);
        currentRotation += 180;
        currentRotation %= 360;

        current = (current + 1) % points.Length;
    }
}
