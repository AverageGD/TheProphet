using UnityEngine;
using BehaviorTree;
using System.Collections;

public class MeleeAttack : Node
{
    private GameObject sickle;
    private Transform meleeAttackPoint;

    public MeleeAttack(GameObject sickle, Transform meleeAttackPoint)
    {
        this.sickle = sickle;
        this.meleeAttackPoint = meleeAttackPoint;
    }

    public override NodeState Evaluate()
    {
        parent.parent.SetData("LastMeleeAttack", Time.time);

        GameObject sickleClone = null;

        Object.Destroy(sickleClone = Object.Instantiate(sickle, meleeAttackPoint), 2f);

        sickleClone.transform.position = meleeAttackPoint.position;

        state = NodeState.Running;
        
        GameManager.instance.StartCoroutine(AttackAction());

        return state;
    }

    private IEnumerator AttackAction()
    {
        parent.parent.SetData("IsMeleeAttacking", true);

        yield return new WaitForSeconds(1f);

        parent.parent.SetData("IsMeleeAttacking", false);
    }
}
