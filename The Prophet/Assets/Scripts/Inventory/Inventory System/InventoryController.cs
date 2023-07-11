using System.Collections;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private bool isInventoryOpen = false;

    public void TryToTurnOnOffInventory() //Turns inventory's UI on/off
    {
        isInventoryOpen = !isInventoryOpen;

        gameObject.SetActive(isInventoryOpen);
    }

}
