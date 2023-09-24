using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField] private GameObject _playerCorpse;

    public short id;
    public Sprite roomSpriteDefault;
    public Vector2 roomSpriteCoordinates;

    private void Start()
    {
        AllRoomsContainer.instance.VisitRoom(id);

        if (RoomsUIController.instance != null)
            RoomsUIController.instance.ListRooms();

        if (SaveManager.instance.DeathRoomID != -1 && id == SaveManager.instance.DeathRoomID)
        {
            GameObject playerCorpseClone = Instantiate(_playerCorpse, transform);
        }
            
    }

    private void FixedUpdate()
    {
        if (SaveManager.instance.roomID != id)
        {
            Destroy(gameObject);
        }
    }
}
