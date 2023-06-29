using UnityEngine;

public class SporeRibbon : Interactable
{
    public override void Interact()
    {
        base.Interact();

        print("Spore Ribbon has been taken");
        _player.GetComponent<Inventory>().AddRibbon(new SporeRibbonLogics("spore_ribbon"));
        Destroy(gameObject);
    }
}

public class SporeRibbonLogics : AbstractRibbon
{
    public SporeRibbonLogics(string iD) : base(iD)
    {
        isWearing = true;
    }

    public override void AbilityInvoker()
    {
        Debug.Log("Ribbon is called"); //here will be ribbon code
    }
}
