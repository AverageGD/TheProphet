using UnityEngine;
using BehaviorTree;

public class CheckStage : Node
{
    private int targetStageNumber;
    private BossHealthController bossHealthController;

    public CheckStage(int stageNumber, BossHealthController bossHealthController)
    {
        this.targetStageNumber = stageNumber;
        this.bossHealthController = bossHealthController;

    }
    public override NodeState Evaluate()
    {
        if (targetStageNumber <= bossHealthController.currentStageNumber)
        {
            state = NodeState.Success;

            return state;
        }

        state = NodeState.Failure;

        return state;
    }
}
