using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private bool isInventoryOpen = false;

    private GameObject descriptionBox;
    public void TryToTurnOnOffInventory() //Turns inventory's UI on/off
    {
        descriptionBox = transform.Find("DescriptionBox").gameObject;

        descriptionBox.SetActive(false);

        isInventoryOpen = !isInventoryOpen;

        gameObject.SetActive(isInventoryOpen);
    }

}
