using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;


    [SerializeField] private Transform _player;

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
        Fade.instance.FadeInvoker();

        yield return new WaitForSeconds(0.74f);

        _player.transform.position = newPosition;
    }
    #endregion

    #region Invincibility logics

    public void InvincibilityInvoker(GameObject gameObject, float time, bool changeTransparency)
    {
        StopAllCoroutines();
        StartCoroutine(Invincibility(gameObject, time, changeTransparency));
    }

    private IEnumerator Invincibility(GameObject gameObject, float time, bool changeTransparency)
    {
        SpriteRenderer spriteRenderer = null;

        LayerMask originalLayer = gameObject.layer;

        if (changeTransparency)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = spriteRenderer.color.WithAlpha(0.5f);
        }

        gameObject.layer = LayerMask.NameToLayer("Invincible");

        yield return new WaitForSeconds(time);

        if (changeTransparency)
            spriteRenderer.color = spriteRenderer.color.WithAlpha(1);

        gameObject.layer = originalLayer;
    }

    #endregion
}
