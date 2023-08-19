using UnityEngine;

public class RoomsUIController : MonoBehaviour
{
    private bool isMapOpen;
    public void TryToTurnOnOffMap()
    {
        isMapOpen = !isMapOpen;

        gameObject.SetActive(isMapOpen);

        ListRooms();

    }


    private void ListRooms()
    {

    }
}
