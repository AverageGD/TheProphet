using UnityEngine;

public class GateLogics : MonoBehaviour
{
    [SerializeField] private int _id;

    private Vector2 currentVelocity = Vector2.zero;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = transform.Find("Target").position;
    }

    private void Update()
    {
        if (GatesController.instance != null)
        {
            if (GatesController.instance.gates[_id])
            {
                transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref currentVelocity, 0.2f);
            } else
            {
                transform.position = Vector2.SmoothDamp(transform.position, startPosition, ref currentVelocity, 0.2f);
            }
        }
    }
}
