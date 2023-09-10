using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : EnemyHealthController
{
    [SerializeField] private int _maxStagesCount;

    public int currentStageNumber = 0;
    private Slider _healthBarUI;

    protected override void Start()
    {
        base.Start();

        _healthBarUI = GameManager.instance.bossHealthBarUI;
        _healthBarUI.maxValue = _maxHealth;
        _healthBarUI.value = health;
    }

    public override void TakeDamage(float damage)
    {
        Flash.instance.FlashSpriteInvoker(spriteRenderer);

        health -= damage;
        StartCoroutine(UpdateHealthBarUI());

        animator.SetFloat("Health", health);

        if (health <= 0 && currentStageNumber == _maxStagesCount - 1)
            StartCoroutine(Die());
        else if (health <= 0)
        {
            ChangePhase();
        }
    }

    private void ChangePhase()
    {
        currentStageNumber++;

        SetHealth(_maxHealth);
    }

    private IEnumerator UpdateHealthBarUI()
    {
        _healthBarUI.maxValue = _maxHealth;

        float healthBarUIChangingVelocity = 0;

        while (_healthBarUI.value != health)
        {
            _healthBarUI.value = Mathf.SmoothDamp(_healthBarUI.value, health, ref healthBarUIChangingVelocity, 0.2f);

            yield return null;
        }
    }

    private void SetHealth(float value)
    {
        health = value;
        health = Mathf.Clamp(health, 0, _maxHealth);
        StopAllCoroutines();
        StartCoroutine(UpdateHealthBarUI());
    }
}
