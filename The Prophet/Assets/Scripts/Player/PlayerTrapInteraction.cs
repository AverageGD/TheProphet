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

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Animator>().Play("Idle");
            PlayerHealthController.instance.TakeDamage(1, Vector2.up, false);

            GameManager.instance.TeleportationInvoker(CharacterController2D.instance.lastSafePosition, true); //Calls teleportation script to the last safe position
        }
    }

}
