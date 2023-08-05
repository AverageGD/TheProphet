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
    private short currentHealTriesCount;
    private bool canHeal = true;

    public short maxHealTriesCount;
    public float recoverableHealth;
    public float maxHealth;

    public float Health { get; private set; }

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

        currentHealTriesCount = maxHealTriesCount;
        Health = maxHealth;

        _healthBarUI.maxValue = maxHealth;
        _healthBarUI.value = Health;
    }

    private IEnumerator UpdateHealthBarUI()
    {

        float healthBarUIChangingVelocity = 0;

        while (_healthBarUI.value != Health)
        {
            _healthBarUI.value = Mathf.SmoothDamp(_healthBarUI.value, Health, ref healthBarUIChangingVelocity, 0.2f);

            yield return null;
        }
    }

    public void TakeDamage(float damage)
    {

        animator.SetBool("IsHealing", false);

        OnHealEnd?.Invoke();
        canHeal = true;

        VibrationController.instance.StartVibration(0.3f, 0.3f, 0.5f);
        GameManager.instance.InvincibilityInvoker(gameObject, 1.5f, true);

        Flash.instance.FlashSpriteInvoker(spriteRenderer);
        Health -= damage;
        StopAllCoroutines();

        StartCoroutine(UpdateHealthBarUI());


        if (Health <= 0)
        {
            Die();
        }
    }

    public void TryToHealInvoker()
    {
        rigidBody.velocity = Vector3.zero;

        if (!canHeal || currentHealTriesCount == 0 || !GameManager.instance.IsGrounded(groundChecker, _groundCheckDistance))
            return;

        StartCoroutine(Heal());
    }

    private IEnumerator Heal()
    {
        animator.SetBool("IsHealing", true);
        canHeal = false;
        currentHealTriesCount--;

        OnHealStart?.Invoke();

        yield return new WaitForSeconds(1.3f);

        Health += recoverableHealth;
        Health = Mathf.Clamp(Health, 0, maxHealth);

        OnHealEnd?.Invoke();

        canHeal = true;
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

        gameObject.SetActive(false);
    }


}
