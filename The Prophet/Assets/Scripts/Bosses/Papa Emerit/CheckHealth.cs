using UnityEngine;
using BehaviorTree;

public class CheckHealth : Node
{
    public ComparatorCriteria criteria;
    public float health;

    public CheckHealth(ComparatorCriteria criteria, float health)
    {
        this.criteria = criteria;
        this.health = health;
    }

    public override NodeState Evaluate()
    {
        if (criteria != null && criteria(health))
        {
            state = NodeState.Success;
        } else
        {
            state = NodeState.Failure;
        }

        return state;
    }

    public delegate bool ComparatorCriteria(float health);
}
