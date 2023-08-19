using System.Collections.Generic;
using UnityEngine;

public class AllRoomsContainer : MonoBehaviour
{
    public static AllRoomsContainer instance;

    [SerializeField] private List <GameObject> rooms = new List<GameObject> ();

    public Dictionary <int, GameObject> roomsDictionary = new Dictionary<int, GameObject> ();
    public Dictionary <int, bool> visitedRoomsDictionary = new Dictionary<int, bool> ();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (GameObject room in rooms)
        {
            roomsDictionary[room.GetComponent<RoomInfo>().id] = room;
            visitedRoomsDictionary[room.GetComponent<RoomInfo>().id] = false;
        }
    }

    public void CreateRoom(short id)
    {
        Instantiate(roomsDictionary[id]);
    }

    public void VisitRoom(short id)
    {
        visitedRoomsDictionary[id] = true;
    }
}
