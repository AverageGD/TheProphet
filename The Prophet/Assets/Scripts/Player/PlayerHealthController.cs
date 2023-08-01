using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Slider _healthBarUI;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private Transform groundChecker;
    private short currentHealTriesCount;
    private bool canHeal = true;

    public short maxHealTriesCount;
    public float health;
    public float recoverableHealth;

    public UnityEvent OnDeath;

    public UnityEvent OnHealStart;
    public UnityEvent OnHealEnd;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        groundChecker = transform.Find("GroundChecker");

        currentHealTriesCount = maxHealTriesCount;
        health = _maxHealth;

        _healthBarUI.maxValue = _maxHealth;
        _healthBarUI.value = health;
    }

    private IEnumerator UpdateHealthBarUI()
    {
        float healthBarUIChangingVelocity = 0;

        while (_healthBarUI.value != health)
        {
            _healthBarUI.value = Mathf.SmoothDamp(_healthBarUI.value, health, ref healthBarUIChangingVelocity, 0.2f);

            yield return new WaitForSeconds(0.001f);
        }
    }

    public void TakeDamage(float damage)
    {
        VibrationController.instance.StartVibration(0.3f, 0.3f, 0.5f);
        GameManager.instance.InvincibilityInvoker(gameObject, 1.5f, true);

        Flash.instance.FlashSpriteInvoker(spriteRenderer);
        health -= damage;

        StartCoroutine(UpdateHealthBarUI());

        if (health <= 0)
        {
            StartCoroutine(Die());
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

        health += recoverableHealth;
        health = Mathf.Clamp(health, 0, _maxHealth);

        OnHealEnd?.Invoke();

        canHeal = true;
        print("Can heal now");
        animator.SetBool("IsHealing", false);

        StartCoroutine(UpdateHealthBarUI());
    }

    private IEnumerator Die()
    {
        OnDeath?.Invoke();

        DeathScreen.instance.CreateSilhouette(transform, spriteRenderer);

        yield return new WaitForSeconds(0.08f);

        gameObject.SetActive(false);
    }


}
