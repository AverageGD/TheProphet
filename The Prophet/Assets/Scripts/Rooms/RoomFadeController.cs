using UnityEngine;
using UnityEngine.UI;

public class RoomFadeController : MonoBehaviour
{
    private GameObject roomFade;


    private void Start()
    {
        roomFade = GameManager.instance.roomFade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (roomFade != null)
                roomFade.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (roomFade != null)
                roomFade.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Color tmp = roomFade.GetComponent<Image>().color;

            float distance = Mathf.Pow(collision.transform.position.x - transform.position.x, 2) + Mathf.Pow(collision.transform.position.y - transform.position.y, 2);
            tmp.a = 1 / Mathf.Sqrt(distance);

            roomFade.GetComponent<Image>().color = tmp;
        }
    }
}
