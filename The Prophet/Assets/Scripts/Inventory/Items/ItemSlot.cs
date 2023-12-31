using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler //Here is described logic for items that we drag to the slots
{
    public Slots slot;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        //keeps corresponding item's controller because we call it several times
        InventoryItemController inventoryItemController = eventData.pointerDrag.gameObject.GetComponent<InventoryItemController>();

        if (inventoryItemController.isWearing) return;

        //just checks if the type of slot and item are the same
        if (eventData.pointerDrag != null && ((slot == Slots.ribbon1 && inventoryItemController.ribbon != null) || 
            (slot != Slots.ribbon1 && inventoryItemController.amulet != null)))
        {
            //before adding new item in the slot we are being sure there id no left any other items
            foreach (Transform child in transform)
            {

                child.GetComponent<InventoryItemController>().isWearing = false;
                if (child.GetComponent<InventoryItemController>().amulet != null)
                    child.GetComponent<InventoryItemController>().amulet.Deactivate();

                InventoryManager.instance.ResetWearedIems(child.GetComponent<InventoryItemController>().id, false);
                Destroy(child.gameObject);
            }

            GameObject eventDataClone = Instantiate(eventData.pointerDrag.gameObject, transform); //clone our item to that slot

            eventDataClone.GetComponent<Image>().enabled = true;
            eventDataClone.GetComponent<Button>().enabled = true;
            eventDataClone.GetComponent<CanvasGroup>().enabled = true;
            eventDataClone.transform.Find("ItemIcon").GetComponent<Image>().enabled = true;


            //keeps corresponding item's dragdrop because we call it several times
            DragDrop dragDrop = eventDataClone.GetComponent<DragDrop>(); 

            dragDrop.canvasGroup.blocksRaycasts = true;
            dragDrop.enabled = false;

            inventoryItemController.isWearing = true;

            if (inventoryItemController.amulet != null)
                inventoryItemController.amulet.Activate();

            InventoryManager.instance.ResetWearedIems(inventoryItemController.id, true);

            eventDataClone.transform.position = transform.position;

            //Calls a function that is responsible for changing wearing items' abilities
            inventoryItemController.WearItem(slot);

        }
    }

    public enum Slots //Just for easy distinction
    {
        ribbon1,
        amulet1,
        amulet2
    }
}
