using UnityEngine;
using UnityEngine.UI;

public class RoomsUIController : MonoBehaviour
{
    public static RoomsUIController instance;

    [SerializeField] private GameObject _roomUI;
    [SerializeField] private Transform _map;

    public bool IsMapOpen { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TryToTurnOnOffMap()
    {
        IsMapOpen = !IsMapOpen;

        gameObject.SetActive(IsMapOpen);

        ListRooms();
    }


    public void ListRooms()
    {
        foreach (Transform room in _map)
        {
            Destroy(room.gameObject);
        }

        foreach (int id in AllRoomsContainer.instance.visitedRooms)
        {
                GameObject roomUIClone = Instantiate(_roomUI, transform);

                roomUIClone.transform.localPosition = AllRoomsContainer.instance.roomsDictionary[id].GetComponent<RoomInfo>().roomSpriteCoordinates;
                roomUIClone.GetComponent<Image>().sprite = AllRoomsContainer.instance.roomsDictionary[id].GetComponent<RoomInfo>().roomSprite;
                roomUIClone.GetComponent<Image>().SetNativeSize();
        }
    }
}
