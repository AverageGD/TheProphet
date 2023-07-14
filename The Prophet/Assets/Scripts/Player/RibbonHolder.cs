using UnityEngine;

public class RibbonHolder : MonoBehaviour
{
    public static RibbonHolder instance;

    public RibbonAbility ribbonAbility;

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
        if (ribbonAbility == null) return; //Checking if ribbon is not weared
        
        switch (state)
        {
            case RibbonState.ready: //if ribbon is ready to use, we will give cooldownTime and activeTime corresponding values
                coolDownTime = ribbonAbility.coolDownTime;
                activeTime = ribbonAbility.activeTime;
            break;

            case RibbonState.active: //if ribbon's ability is already active, we need to decrease active time
                if (activeTime > 0)
                    activeTime -= Time.deltaTime;
                else
                {
                    state = RibbonState.coolDown;
                    coolDownTime = ribbonAbility.coolDownTime;
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
        if (state == RibbonState.ready && ribbonAbility != null) //checks if ribbon is ready to use
        {
            state = RibbonState.active; //changes the state to active
            ribbonAbility.Activate(); //calls ribbon's function
        }
    }
}
