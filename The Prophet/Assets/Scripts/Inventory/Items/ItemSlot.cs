using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public Slots slot;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        InventoryItemController inventoryItemController = eventData.pointerDrag.gameObject.GetComponent<InventoryItemController>();

        if (eventData.pointerDrag != null && ((slot == Slots.ribbon1 && inventoryItemController.ribbon != null) || 
            (slot != Slots.ribbon1 && inventoryItemController.amulet != null)))
        {
            foreach (Transform child in transform)
            {
                Destroy(child);
            }

            GameObject eventDataClone = Instantiate(eventData.pointerDrag.gameObject, transform);


            DragDrop dragDrop = eventDataClone.GetComponent<DragDrop>();
            dragDrop.canvasGroup.blocksRaycasts = true;
            dragDrop.enabled = false;

            eventDataClone.transform.position = transform.position;

            inventoryItemController.WearItem(slot);

        }
    }

    public enum Slots
    {
        ribbon1,
        amulet1,
        amulet2
    }
}
