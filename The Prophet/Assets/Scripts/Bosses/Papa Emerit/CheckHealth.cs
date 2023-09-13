using UnityEngine;
using BehaviorTree;

public class CheckHealth : Node
{
    public ComparatorCriteria criteria;
    public BossHealthController bossHealthController;

    public CheckHealth(ComparatorCriteria criteria, BossHealthController bossHealthController)
    {
        this.criteria = criteria;
        this.bossHealthController = bossHealthController;
    }

    public override NodeState Evaluate()
    {
        if (criteria != null && criteria(bossHealthController.health))
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
