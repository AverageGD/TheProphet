using BehaviorTree;
using UnityEngine;

public class LookAtPlayer : Node
{
    private Transform transform;

    public LookAtPlayer(Transform transform)
    {
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        if ((bool)parent.GetData("IsMeleeAttacking"))
        {
            state = NodeState.Failure;
            return state;
        }

        if (CharacterController2D.instance != null && transform.position.x - CharacterController2D.instance.transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        state = NodeState.Running;
        return state;
    }
}
