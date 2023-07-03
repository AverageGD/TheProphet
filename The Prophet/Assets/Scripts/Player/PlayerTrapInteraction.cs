using System.Collections;
using UnityEngine;

public class PlayerTrapInteraction : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap")) //Checks if this is a trap
        {
            print("trapped");

            //also here will be taking damage for the player

            GameManager.instance.TeleportationInvoker(gameObject.GetComponent<CharacterController2D>().lastSafePosition); //Calls teleportation script to the last safe position
        }
    }

}
