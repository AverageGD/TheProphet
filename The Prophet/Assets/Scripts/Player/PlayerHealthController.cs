using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Slider _healthBarUI;
    [SerializeField] private Color _deathScreenCustomColor;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Transform groundChecker;
    private bool canHeal = true;
    private bool isDead = false;

    public float health;
    public short maxHealTriesCount;
    public short currentHealTriesCount;
    public float recoverableHealth;
    public float maxHealth;
    public bool isHealing;
    public bool isHeartOfTheRomaUsed;
   

    public UnityEvent OnDeath;

    public UnityEvent OnHealStart;
    public UnityEvent OnHealEnd;


    private void Awake()
    {

        if (instance == null)
            instance = this;

        isDead = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        groundChecker = transform.Find("GroundChecker");
    }

    private void Start()
    {
        currentHealTriesCount = maxHealTriesCount;
        health = maxHealth;

        _healthBarUI.maxValue = maxHealth;
        _healthBarUI.value = health;

        HealPotionsBarController.instance.UpdateHealPotions();
    }
    public void SetHealth(float value)
    {
        health = value;
        health = Mathf.Clamp(health, 0, maxHealth);
        StopAllCoroutines();
        StartCoroutine(UpdateHealthBarUI());
    }

    private IEnumerator UpdateHealthBarUI()
    {
        _healthBarUI.maxValue = maxHealth;

        float healthBarUIChangingVelocity = 0;

        while (_healthBarUI.value != health)
        {
            _healthBarUI.value = Mathf.SmoothDamp(_healthBarUI.value, health, ref healthBarUIChangingVelocity, 0.2f);

            yield return null;
        }
    }

    public void TakeDamage(float damage, Vector2 hitDirection, bool needKnockback)
    {
        if (animator != null)
            animator.SetBool("IsHealing", false);

        OnHealEnd?.Invoke();
        canHeal = true;
        isHealing = false;

        VibrationController.instance.StartVibration(0.3f, 0.3f, 0.5f);

        if (gameObject != null)
            GameManager.instance.InvincibilityInvoker(gameObject, 0.5f, true);

        Flash.instance.FlashSpriteInvoker(spriteRenderer);
        health -= damage;
        StopAllCoroutines();

        StartCoroutine(UpdateHealthBarUI());


        if (!isDead && health <= 0)
        {
            Die();
            return;
        }

        if (needKnockback)
            Knockback.instance.KnockbackInvoker(hitDirection, Vector2.up, Input.GetAxisRaw("Horizontal"));
    }

    public void TryToHealInvoker()
    {
        if (!canHeal || currentHealTriesCount == 0 || !GameManager.instance.IsGrounded(groundChecker, _groundCheckDistance))
            return;

        StartCoroutine(Heal());
    }

    private IEnumerator Heal()
    {
        rigidBody.velocity = Vector3.zero;
        animator.SetBool("IsHealing", true);

        canHeal = false;
        isHealing = true;
        currentHealTriesCount--;

        HealPotionsBarController.instance.UpdateHealPotions();

        OnHealStart?.Invoke();

        yield return new WaitForSeconds(1.3f);

        health += recoverableHealth;
        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealEnd?.Invoke();

        canHeal = true;
        isHealing = false;

        print("Can heal now");
        animator.SetBool("IsHealing", false);

        StartCoroutine(UpdateHealthBarUI());
    }

    private void Die()
    {
        isDead = true;

        print("Dead");

        if ((isHeartOfTheRomaUsed && Random.Range(0, 4) != 0) || !isHeartOfTheRomaUsed)
        {
            SaveManager.instance.SaveDeathInfo(SaveManager.instance.roomID);

            PlayerCurrencyController.instance.currency = 0;

        }

        SaveManager.instance.SavePlayerCurrency();

        OnDeath?.Invoke();

        DeathScreen.instance.gameObject.GetComponent<Image>().color = _deathScreenCustomColor;
        DeathScreen.instance.CreateSilhouette(transform, spriteRenderer);

        GlobalFade.instance.FadeIn();
        DeathMessage.instance.ShowMessage();

        Destroy(gameObject);
    }


}
