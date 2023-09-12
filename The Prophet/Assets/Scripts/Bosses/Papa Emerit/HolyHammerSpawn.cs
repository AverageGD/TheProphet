using BehaviorTree;
using UnityEngine;

public class HolyHammerSpawn : Node
{
    public GameObject holyHammer;

    public HolyHammerSpawn(GameObject holyHammer)
    {
        this.holyHammer = holyHammer;
    }

    public override NodeState Evaluate()
    {
        parent.parent.SetData("LastHolyHammer", Time.time);
        GameObject holyHammerClone = Object.Instantiate(holyHammer);

        holyHammerClone.transform.position = new Vector2(holyHammerClone.transform.position.x, -103);

        Object.Destroy(holyHammerClone, 3f);

        state = NodeState.Running;

        return state;
    }
}
