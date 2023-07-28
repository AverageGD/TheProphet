using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen instance;

    [SerializeField] private GameObject _silhouettePrefab;
    [SerializeField] private GameObject _deathHit;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void CreateSilhouette(Transform transform, SpriteRenderer spriteRenderer)
    {
        if (spriteRenderer == null)
            return;

        GameObject silhouetteClone = Instantiate(_silhouettePrefab, this.transform);

        silhouetteClone.transform.position = transform.position;
        silhouetteClone.transform.localScale = transform.localScale;

        silhouetteClone.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;

        if (spriteRenderer.flipX || transform.rotation.y != 0f)
        {
            silhouetteClone.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (transform.CompareTag("Player"))
            CreateDeathHit(transform);
    }

    private void CreateDeathHit(Transform transform)
    {
        GameObject deathHitClone = Instantiate(_deathHit);
        deathHitClone.transform.position = transform.position;
        Destroy(deathHitClone, 0.15f);
    }
}
