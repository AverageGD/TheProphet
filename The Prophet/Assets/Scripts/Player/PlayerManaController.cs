using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManaController : MonoBehaviour
{
    public static PlayerManaController instance;

    [SerializeField] private Slider _manaBarUI;

    public float maxMana;
    public float Mana { get; private set; }


    private void Awake()
    {

        if (instance == null)
            instance = this;

        Mana = maxMana;
        _manaBarUI.maxValue = maxMana;
        _manaBarUI.value = maxMana;

    }

    public void SpendMana(float spendingMana)
    {
        Mana -= spendingMana;
        Mana = Mathf.Clamp(Mana, 0, maxMana);

        StartCoroutine(UpdateManaBarUI());
    }

    public void ReceiveMana(float receivingMana) 
    {
        Mana += receivingMana;

        Mana = Mathf.Clamp(Mana, 0, maxMana);

        StartCoroutine(UpdateManaBarUI());
    }

    private IEnumerator UpdateManaBarUI()
    {
        _manaBarUI.maxValue = maxMana;

        float manaBarUIChangingVelocity = 0;

        while (_manaBarUI.value != Mana)
        {
            _manaBarUI.value = Mathf.SmoothDamp(_manaBarUI.value, Mana, ref manaBarUIChangingVelocity, 0.2f);

            yield return new WaitForSeconds(0.001f);
        }
    }
}
