using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private bool isInventoryOpen = false;
    public void TurnOnOffInventory() //Turns inventory UI on/off
    {
        isInventoryOpen = !isInventoryOpen;
        gameObject.SetActive(isInventoryOpen);
    }
}
