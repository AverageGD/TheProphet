using UnityEngine;

public class PlayerGhostSpawnLogic : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    [SerializeField] private SpriteRenderer _ghostSpriteRenderer;

    private void Start()
    {
        _ghostSpriteRenderer.sprite = _player.GetComponent<SpriteRenderer>().sprite;
    }
}
