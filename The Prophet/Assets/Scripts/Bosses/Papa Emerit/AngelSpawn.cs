using BehaviorTree;
using UnityEngine;

public class AngelSpawn : Node
{
    public GameObject angels;

    public AngelSpawn(GameObject angels)
    {
        this.angels = angels;
    }

    public override NodeState Evaluate()
    {
        parent.parent.SetData("LastAngelAttack", Time.time);

        angels.SetActive(true);

        state = NodeState.Running;

        return state;
    }
}
