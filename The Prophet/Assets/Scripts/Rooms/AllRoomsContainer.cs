using System.Collections.Generic;
using UnityEngine;

public class AllRoomsContainer : MonoBehaviour
{
    public static AllRoomsContainer instance;

    [SerializeField] private List <GameObject> rooms = new List<GameObject> ();

    public Dictionary <int, GameObject> roomsDictionary = new Dictionary<int, GameObject> ();
    public List <int> visitedRooms = new List<int>();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (GameObject room in rooms)
        {
            roomsDictionary[room.GetComponent<RoomInfo>().id] = room;
        }
    }

    public void CreateRoom(short id)
    {
        Instantiate(roomsDictionary[id]);
    }

    public void VisitRoom(short id)
    {
        Debug.Log("check room");
        if (visitedRooms.Contains(id))
            return;

        Debug.Log("added room");
        visitedRooms.Add(id);
        SaveManager.instance.SaveVisitedRooms();
    }
}
