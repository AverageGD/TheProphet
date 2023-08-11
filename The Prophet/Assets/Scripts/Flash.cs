using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _flashMaterial;
    [SerializeField] private Material _defaultMaterial;

    public static Flash instance;

    private void Start()
    {
        instance = this;
    }
    public void FlashSpriteInvoker(SpriteRenderer spriteRenderer)
    {
        StartCoroutine(FlashSprite(spriteRenderer));
    }

    private IEnumerator FlashSprite(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.material = _flashMaterial;

        yield return new WaitForSeconds(0.05f);
        if (spriteRenderer != null)
            spriteRenderer.material = _defaultMaterial;
    }
}
