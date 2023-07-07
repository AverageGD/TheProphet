using UnityEngine;

public class RibbonHolder : MonoBehaviour
{
    public static RibbonHolder instance;

    public RibbonAbility ribbon;

    private float coolDownTime;
    private float activeTime;

    enum RibbonState
    {
        ready,
        active,
        coolDown
    }

    private RibbonState state = RibbonState.ready;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (ribbon == null) return; //Checking if ribbon is not weared
        
        switch (state)
        {
            case RibbonState.ready: //if ribbon is ready to use, we will give cooldownTime and activeTime corresponding values
                coolDownTime = ribbon.coolDownTime;
                activeTime = ribbon.activeTime;
            break;

            case RibbonState.active: //if ribbon's ability is already active, we need to decrease active time
                if (activeTime > 0)
                    activeTime -= Time.deltaTime;
                else
                {
                    state = RibbonState.coolDown;
                    coolDownTime = ribbon.coolDownTime;
                }
           break;

            case RibbonState.coolDown:
                if (coolDownTime > 0)
                    coolDownTime -= Time.deltaTime;
                else
                    state = RibbonState.ready;
           break;
        }
    }

    public void ActivateAbility() //When player clicka on the Ribbon's ability button(RMB or LB), engine calls this function
    {
        if (state == RibbonState.ready) //checks if ribbon is ready to use
        {
            state = RibbonState.active; //changes the state to active
            ribbon.Activate(); //calls ribbon's function
        }
    }
}
