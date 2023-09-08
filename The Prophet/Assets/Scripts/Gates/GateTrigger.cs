using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private int _gateId;
    [SerializeField] private bool _condition;

    private void Start()
    {
        GatesController.instance.gates[_gateId] = !_condition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GatesController.instance.gates[_gateId] = _condition;
            Destroy(gameObject);
        }
    }
}
