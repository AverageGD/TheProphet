using UnityEngine;

public class ItemPickUp : Interactable //overrides interact function for picking item up
{
    public Item item;

    private void Update()
    {
        if (InventoryManager.instance.Contains(item.id))
            Destroy(gameObject);
    }

    public override void Interact()
    {
        base.Interact();

        InventoryManager.instance.Add(item);

        InventoryManager.instance.ListItems();
        Destroy(gameObject);
    }
}
