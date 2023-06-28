using UnityEngine;

public class SporeRibbon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Inventory>().AddRibbon(new SporeRibbonLogics("spore_ribbon"));
            Destroy(gameObject);
        }
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
