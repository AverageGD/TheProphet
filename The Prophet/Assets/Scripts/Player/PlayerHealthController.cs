using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Slider _healthBarUI;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Transform groundChecker;
    private bool canHeal = true;

    public float health;
    public short maxHealTriesCount;
    public short currentHealTriesCount;
    public float recoverableHealth;
    public float maxHealth;
    public bool isHealing;

   

    public UnityEvent OnDeath;

    public UnityEvent OnHealStart;
    public UnityEvent OnHealEnd;


    private void Awake()
    {

        if (instance == null)
            instance = this;

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

    private IEnumerator UpdateHealthBarUI()
    {

        float healthBarUIChangingVelocity = 0;

        while (_healthBarUI.value != health)
        {
            _healthBarUI.value = Mathf.SmoothDamp(_healthBarUI.value, health, ref healthBarUIChangingVelocity, 0.2f);

            yield return null;
        }
    }

    public void TakeDamage(float damage)
    {
        if (animator != null)
            animator.SetBool("IsHealing", false);

        OnHealEnd?.Invoke();
        canHeal = true;

        VibrationController.instance.StartVibration(0.3f, 0.3f, 0.5f);
        if (gameObject != null)
            GameManager.instance.InvincibilityInvoker(gameObject, 1.5f, true);

        Flash.instance.FlashSpriteInvoker(spriteRenderer);
        health -= damage;
        StopAllCoroutines();

        StartCoroutine(UpdateHealthBarUI());


        if (health <= 0)
        {
            Die();
        }
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
        OnDeath?.Invoke();

        DeathScreen.instance.CreateSilhouette(transform, spriteRenderer);

        GlobalFade.instance.FadeIn();
        DeathMessage.instance.ShowMessage();

        Destroy(gameObject);
    }


}
