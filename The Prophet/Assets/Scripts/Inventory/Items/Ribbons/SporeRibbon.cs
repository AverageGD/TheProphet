using TMPro;
using UnityEngine;

public class SporeRibbon : Interactable
{
    public override void Interact()
    {
        base.Interact();

        print("Spore Ribbon has been taken");
        
        Destroy(gameObject);
    }
}
