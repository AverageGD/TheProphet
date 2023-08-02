using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaController : MonoBehaviour
{
    public static PlayerManaController instance;

    [SerializeField] private float _maxMana;
    [SerializeField] private Slider _manaBarUI;
    public float Mana { get; private set; }


    private void Start()
    {

        if (instance == null)
            instance = this;

        Mana = _maxMana;
        _manaBarUI.maxValue = _maxMana;
        _manaBarUI.value = _maxMana;

    }

    public void SpendMana(float spendingMana)
    {
        Mana -= spendingMana;
        Mana = Mathf.Clamp(Mana, 0, _maxMana);

        StartCoroutine(UpdateManaBarUI());
    }

    public void ReceiveMana(float receivingMana) 
    {
        Mana += receivingMana;

        Mana = Mathf.Clamp(Mana, 0, _maxMana);

        StartCoroutine(UpdateManaBarUI());
    }

    private IEnumerator UpdateManaBarUI()
    {

        float manaBarUIChangingVelocity = 0;

        while (_manaBarUI.value != Mana)
        {
            _manaBarUI.value = Mathf.SmoothDamp(_manaBarUI.value, Mana, ref manaBarUIChangingVelocity, 0.2f);

            yield return new WaitForSeconds(0.001f);
        }
    }
}
