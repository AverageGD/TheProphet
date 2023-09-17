using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : EnemyHealthController
{
    [SerializeField] private int _maxStagesCount;
    [SerializeField] private Color _deathScreenCustomColor;

    public int currentStageNumber = 0;
    public int id;

    private GameObject worldCanvas;
    private Slider healthBarUI;
    private GameObject bossHealthBarBorder;

    protected override void Start()
    {
        base.Start();

        healthBarUI = GameManager.instance.bossInfo.Find("BossHealthBar").GetComponent<Slider>();
        bossHealthBarBorder = GameManager.instance.bossInfo.Find("BossBorder").gameObject;
        healthBarUI.maxValue = _maxHealth;
        healthBarUI.value = health;
        worldCanvas = GameManager.instance.worldCanvas;
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
        healthBarUI.maxValue = _maxHealth;

        float healthBarUIChangingVelocity = 0;

        while (healthBarUI.value != health)
        {
            healthBarUI.value = Mathf.SmoothDamp(healthBarUI.value, health, ref healthBarUIChangingVelocity, 0.2f);

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

    protected override IEnumerator Die()
    {
        OnDeath?.Invoke();

        gameObject.layer = LayerMask.NameToLayer("Invincible");

        worldCanvas.SetActive(true);

        healthBarUI.gameObject.SetActive(false);
        bossHealthBarBorder.SetActive(false);

        DeathScreen.instance.gameObject.GetComponent<Image>().color = _deathScreenCustomColor;

        DeathScreen.instance.CreateSilhouette(transform, spriteRenderer);

        yield return new WaitForSeconds(0.5f);

        LocalFade.instance.LocalFadeInvoker();

        yield return new WaitForSeconds(1f);

        foreach (Transform child in worldCanvas.transform)
        {
            Destroy(child.gameObject);
        }

        worldCanvas.SetActive(false);
        
        PlayerCurrencyController.instance.AddCurrency((int)(PlayerCurrencyController.instance.currencyMultiplier * Random.Range(_currencyMinimum, _currencyMaximum)));

        BossManager.instance.beatenBosses.Add(id);
        SaveManager.instance.SaveBeatenBosses();

        Destroy(transform.parent.gameObject);
    }
}
