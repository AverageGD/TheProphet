using DG.Tweening;
using UnityEngine;

public class PlayerCorpseController : Interactable
{
    private int currency;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();

        transform.localPosition = SaveManager.instance.playerCorpsePosition;
        currency = SaveManager.instance.CorpseCurrency;
    }

    public override void Interact()
    {
        base.Interact();

        collider.enabled = false;

        PlayerCurrencyController.instance.AddCurrency(currency);

        SaveManager.instance.SaveDeathInfo(-1);
        
        spriteRenderer.DOFade(0, 0.2f);

        Destroy(gameObject, 0.3f);

    }
}
