using System.Collections;
using UnityEngine;

public class DoorController : Interactable
{
    [SerializeField] private GameObject _newRoom;

    private Transform teleportationTarget;
    private GameObject currentRoom;

    private void Start()
    {
        teleportationTarget = transform.Find("TeleportationTarget");

        currentRoom = transform.parent.parent.gameObject;
    }

    public override void Interact()
    {
        base.Interact();

        if (!LocalFade.instance.gameObject.activeSelf)
            StartCoroutine(ChangeRoom());

    }

    private IEnumerator ChangeRoom()
    {
        GameManager.instance.TeleportationInvoker(teleportationTarget.position, true);

        yield return new WaitForSeconds(0.74f);

        Instantiate(_newRoom);

        SaveManager.instance.roomID = _newRoom.GetComponent<RoomInfo>().id;

        Destroy(currentRoom);
    }
}
