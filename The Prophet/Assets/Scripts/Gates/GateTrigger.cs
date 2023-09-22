using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private int _gateId;
    [SerializeField] private bool _condition;
    [SerializeField] private bool _isBossFight;
    [SerializeField] private string _bossName;

    public UnityEvent OnTriggerEnter;

    private GameObject bossInfo;

    private void Start()
    {
        GatesController.instance.gates[_gateId] = !_condition;
        bossInfo = GameManager.instance.bossInfo.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            OnTriggerEnter?.Invoke();
            OSTManager.instance.needAmbient = false;
            GatesController.instance.gates[_gateId] = _condition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isBossFight && collision.transform.CompareTag("Player"))
        {
            bossInfo.SetActive(true);

            bossInfo.transform.Find("BossName").GetComponent<Text>().text = _bossName;
        }
    }
}
