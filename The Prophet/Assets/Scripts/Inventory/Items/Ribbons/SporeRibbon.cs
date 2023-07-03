using UnityEngine;

public class SporeRibbon : Interactable
{
    public override void Interact()
    {
        base.Interact();

        print("Spore Ribbon has been taken");
        Inventory.instance.AddRibbon(new SporeRibbonLogics("spore_ribbon"));
        Destroy(gameObject);
    }
}

public class SporeRibbonLogics : AbstractRibbon
{
    public SporeRibbonLogics(string iD) : base(iD)
    {
        isWearing = true; //temporary, just for demonstration
    }

    public override void AbilityInvoker()
    {
        Debug.Log("Ribbon is called"); //here will be ribbon's ability code
    }
}
