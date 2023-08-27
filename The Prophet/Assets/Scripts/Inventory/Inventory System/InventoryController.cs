using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public bool IsInventoryOpen {get; set;}

    private GameObject descriptionBox;
    public void TryToTurnOnOffInventory() //Turns inventory's UI on/off
    {
        descriptionBox = transform.Find("DescriptionBox").gameObject;

        descriptionBox.SetActive(false);

        IsInventoryOpen = !IsInventoryOpen;

        gameObject.SetActive(IsInventoryOpen);
    }

}
