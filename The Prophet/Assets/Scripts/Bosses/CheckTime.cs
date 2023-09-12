using UnityEngine;
using BehaviorTree;

public class CheckTime : Node
{
    private float coolDown;
    private string name;

    private bool firstTime = true;

    public CheckTime(float coolDown, string name)
    {
        this.coolDown = coolDown;
        this.name = name;
    }

    public override NodeState Evaluate()
    {
        if (Time.time - (float)parent.parent.GetData(name) >= coolDown || firstTime)
        {
            state = NodeState.Success;
            firstTime = false;
        }
        else
            state = NodeState.Failure;

        return state;
    }
}
