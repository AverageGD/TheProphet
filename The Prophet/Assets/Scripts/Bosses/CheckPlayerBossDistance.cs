using BehaviorTree;
using UnityEngine;

public class CheckPlayerBossDistance : Node
{
    private Transform transform;
    private float checkDistance;

    public CheckPlayerBossDistance(Transform transform, float checkDistance)
    {
        this.transform = transform;
        this.checkDistance = checkDistance;
    }

    public override NodeState Evaluate()
    {
        if (CharacterController2D.instance != null && Vector2.Distance(transform.position, CharacterController2D.instance.transform.position) <= checkDistance)
        {
            state = NodeState.Success;
            return state;
        }   
        
        state = NodeState.Failure;
        return state;
            

    }

}
