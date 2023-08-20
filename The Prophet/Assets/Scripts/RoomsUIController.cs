using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomsUIController : MonoBehaviour
{
    public static RoomsUIController instance;

    [SerializeField] private GameObject roomUI;
    [SerializeField] private Transform map;

    private bool isMapOpen;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TryToTurnOnOffMap()
    {
        isMapOpen = !isMapOpen;

        gameObject.SetActive(isMapOpen);

        ListRooms();
    }


    public void ListRooms()
    {
        foreach (Transform room in map)
        {
            Destroy(room.gameObject);
        }

        foreach (KeyValuePair<int, bool> key in AllRoomsContainer.instance.visitedRoomsDictionary)
        {
            if (key.Value)
            {
                GameObject roomUIClone = Instantiate(roomUI, transform);

                roomUIClone.transform.localPosition = AllRoomsContainer.instance.roomsDictionary[key.Key].GetComponent<RoomInfo>().roomSpriteCoordinates;
                roomUIClone.GetComponent<Image>().sprite = AllRoomsContainer.instance.roomsDictionary[key.Key].GetComponent<RoomInfo>().roomSprite;
                roomUIClone.GetComponent<Image>().SetNativeSize();
            }

        }
    }
}
