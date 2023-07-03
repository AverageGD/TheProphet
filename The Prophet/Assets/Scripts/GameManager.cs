using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _player;

    public static GameManager instance;

    private void Start()
    {
        instance = this;
    }

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
}
