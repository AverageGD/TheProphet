using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        InventoryManager.instance.Add(item);
        Destroy(gameObject);
    }
}
