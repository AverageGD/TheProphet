using UnityEngine;
using BehaviorTree;

public class CheckStage : Node
{
    private int targetStageNumber;
    private int currentStageNumber;

    public CheckStage(int stageNumber, int currentStageNumber)
    {
        this.targetStageNumber = stageNumber;
        this.currentStageNumber = currentStageNumber;
    }
    public override NodeState Evaluate()
    {
        if (targetStageNumber >= currentStageNumber)
        {
            state = NodeState.Success;

            return state;
        }

        state = NodeState.Failure;

        return state;
    }
}
