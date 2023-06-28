using UnityEngine;

public class RandomRibbon: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Inventory>().AddRibbon(new SporeRibbonLogics("spore_ribbon"));
            Destroy(gameObject);
        }
    }
}

public class CurrentRibbonLogics1 : AbstractRibbon
{
    public CurrentRibbonLogics1(string iD) : base(iD)
    {
        isWearing = true;
    }

    public override void AbilityInvoker()
    {
        Debug.Log("Ribbon is called");
    }
}
