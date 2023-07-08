using UnityEngine;

public class ItemPickUp : Interactable //overrides interact function for picking item up
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        InventoryManager.instance.Add(item);

        InventoryManager.instance.ListItems();
        Destroy(gameObject);
    }
}
