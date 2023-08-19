using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    public short id;
    public Sprite roomSprite;
    public Vector2 roomSpriteCoordinates;

    private void Start()
    {
        AllRoomsContainer.instance.VisitRoom(id);
    }
}
