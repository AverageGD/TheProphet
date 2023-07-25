using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ThePriestPatrolling : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _patrollingTime;

    public Vector3[] points;
    public int current;

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

            short direction = (short)Mathf.Sign(points[current].x - transform.position.x);

            if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
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

        current++;
        current %= points.Length;

    }
}
