using UnityEngine;
using UnityEngine.Events;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private int _gateId;
    [SerializeField] private bool _condition;
    [SerializeField] private bool _isBossFight;

    public UnityEvent OnTriggerEnter;

    private GameObject bossHealthBarUI;
    private GameObject bossHealthBarBorder;

    private void Start()
    {
        GatesController.instance.gates[_gateId] = !_condition;

        bossHealthBarUI = GameManager.instance.bossHealthBarUI.gameObject;
        bossHealthBarBorder = GameManager.instance.bossHealthBarBorder;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnTriggerEnter?.Invoke();

            if (_isBossFight)
            {
                bossHealthBarUI.SetActive(true);
                bossHealthBarBorder.SetActive(true);
            }
            GatesController.instance.gates[_gateId] = _condition;
        }
    }
}
