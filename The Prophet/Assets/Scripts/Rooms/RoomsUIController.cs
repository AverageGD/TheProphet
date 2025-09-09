using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class RoomsUIController : MonoBehaviour
{
    public static RoomsUIController instance;

    [SerializeField] private GameObject _gui;

    [SerializeField] private GameObject _roomUI;
    [SerializeField] private Transform _map;

    public bool IsMapOpen { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TurnOnOffMap()
    {
        IsMapOpen = !IsMapOpen;

        gameObject.SetActive(IsMapOpen);

        _gui.SetActive(!IsMapOpen);

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
            if (AllRoomsContainer.instance.roomsDictionary[id].GetComponent<RoomInfo>().roomSpriteDefault == null)
                return;

                GameObject roomUIClone = Instantiate(_roomUI, transform);

                roomUIClone.transform.localPosition = AllRoomsContainer.instance.roomsDictionary[id].GetComponent<RoomInfo>().roomSpriteCoordinates;
                roomUIClone.GetComponent<Image>().sprite = AllRoomsContainer.instance.roomsDictionary[id].GetComponent<RoomInfo>().roomSpriteDefault;

                roomUIClone.GetComponent<Image>().SetNativeSize();
        }
    }
}
