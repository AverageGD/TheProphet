using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _groundLayer;

    private void Start()
    {
        instance = this;
    }

    #region Teleportation logics
    public void TeleportationInvoker(Vector2 newPosition) //Calls the coroutine of teleportation to the new position
    {
        StartCoroutine(Teleportation(newPosition));
    }
    private IEnumerator Teleportation(Vector2 newPosition) //Calls fade, waits 0.74 seconds and changes player's position
    {
        LocalFade.instance.LocalFadeInvoker();

        yield return new WaitForSeconds(0.74f);

        if (_player != null)
            _player.transform.position = newPosition;
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
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

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
