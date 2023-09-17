using BehaviorTree;
using System.Collections;
using UnityEngine;

public class RayOfLightAttack : Node
{
    private int rayMaxCount;
    private GameObject ray;
    private Transform bossElements;

    public RayOfLightAttack(int rayMaxCount, GameObject ray, Transform bossElements)
    {
        this.rayMaxCount = rayMaxCount;
        this.ray = ray;
        this.bossElements = bossElements;
    }

    public override NodeState Evaluate()
    {
        parent.parent.SetData("LastRayOfLightAttack", Time.time);

        GameManager.instance.StartCoroutine(RayOfLightAttackAction());

        state = NodeState.Running;

        return state;
    }

    private IEnumerator RayOfLightAttackAction()
    {
        int rayCount = 0;

        while (rayCount < rayMaxCount)
        {
            GameObject rayClone = Object.Instantiate(ray, bossElements);

            if (CharacterController2D.instance != null)
                rayClone.transform.position = CharacterController2D.instance.transform.position;

            Object.Destroy(rayClone, 2f);

            rayCount++;

            yield return new WaitForSeconds(0.8f);
        }
    }
}
