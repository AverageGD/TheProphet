using UnityEngine;

public class RoomChanger : MonoBehaviour
{
    [SerializeField] private short _roomID;

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
            GameManager.instance.TeleportationInvoker(teleportationTarget.position, false);

            AllRoomsContainer.instance.CreateRoom(_roomID);

            SaveManager.instance.roomID = _roomID;

            Destroy(currentRoom);

        }
    }
}
