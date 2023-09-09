using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _groundLayer;


    public Slider bossHealthBarUI;
    public GameObject bossHealthBarBorder;

    public GameObject roomFade;

    public UnityEvent OnTeleportStart;
    public UnityEvent OnTeleportEnd;

    private void Awake()
    {
        instance = this;
    }

    #region Teleportation logics
    public void TeleportationInvoker(Vector2 newPosition, bool needFade) //Calls the coroutine of teleportation to the new position
    {
        StartCoroutine(Teleportation(newPosition, needFade));
    }
    private IEnumerator Teleportation(Vector2 newPosition, bool needFade) //Calls fade, waits 0.74 seconds and changes player's position
    {
        if (needFade)
        {
            LocalFade.instance.LocalFadeInvoker();

            yield return new WaitForSeconds(0.74f);
        }

        if (_player != null)
        {
            OnTeleportStart?.Invoke();

            _player.transform.position = newPosition;
            Camera.main.transform.position = newPosition;

            yield return new WaitForSeconds(0.1f);

            OnTeleportEnd?.Invoke();
        }
    }
    #endregion

    #region Invincibility logics

    public void InvincibilityInvoker(GameObject gameObject, float time, bool changeTransparency)
    {
        StartCoroutine(Invincibility(gameObject, time, changeTransparency));
    }

    private IEnumerator Invincibility(GameObject gameObject, float time, bool changeTransparency)
    {
        SpriteRenderer spriteRenderer = null;

        LayerMask originalLayer = LayerMask.NameToLayer("Player");

        if (gameObject.CompareTag("Enemy"))
            originalLayer = LayerMask.NameToLayer("Enemy");

        if (changeTransparency)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            Color tmp = spriteRenderer.color;
            tmp.a = 0.5f;
            spriteRenderer.color = tmp;
        }

        gameObject.layer = LayerMask.NameToLayer("Invincible");

        yield return new WaitForSeconds(time);

        if (changeTransparency && spriteRenderer != null)
        {
            Color tmp = spriteRenderer.color;
            tmp.a = 1f;
            spriteRenderer.color = tmp;
        }

        if (gameObject != null)
            gameObject.layer = originalLayer;
    }

    #endregion

    #region Freeze logics
    public void FreezeRigidbodyInvoker(float n, Rigidbody2D rigidBody)
    {
        StartCoroutine(FreezeRigidbody(n, rigidBody));
    }
    private IEnumerator FreezeRigidbody(float n, Rigidbody2D rigidBody)
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds(n);

        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    #endregion

    #region SceneReloading logics
    
    public void ReloadCurrentSceneInvoker(float delay)
    {
        StartCoroutine(ReloadCurrentScene(delay));
    }
    private IEnumerator ReloadCurrentScene(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
    public bool IsGrounded(Transform groundChecker, float groundCheckDistance)
    {
        return Physics2D.OverlapCircle(groundChecker.position, groundCheckDistance, _groundLayer);
    }
}
