using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] private GameObject _newRoom;

    private Transform teleportationTarget;
    private GameObject currentRoom;

    private void Start()
    {
        teleportationTarget = transform.Find("TeleportationTarget");

        currentRoom = transform.parent.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.TeleportationInvoker(teleportationTarget.position);

            Instantiate(_newRoom);

            Destroy(currentRoom);

        }
    }
}
