using BehaviorTree;
using System.Collections;
using UnityEngine;

public class PapaEmeritTeleport : Node
{
    public SpriteRenderer spriteRenderer;
    public Transform transform;

    public PapaEmeritTeleport(SpriteRenderer spriteRenderer, Transform transform)
    {
        this.spriteRenderer = spriteRenderer;
        this.transform = transform;
    }

    public override NodeState Evaluate()
    {
        parent.parent.SetData("LastTeleport", Time.time);

        GameManager.instance.StartCoroutine(Teleport());

        state = NodeState.Success;

        return state;
    }

    private IEnumerator Teleport()
    {

        Debug.Log("Start TP");

        while (spriteRenderer.color.a > 0)
        {
            Color tmp = spriteRenderer.color;
            tmp.a = spriteRenderer.color.a - Time.deltaTime;
            spriteRenderer.color = tmp;

            yield return null;
        }

        transform.position = new Vector2(Random.Range(-60, -34), transform.position.y);

        while (spriteRenderer.color.a < 100)
        {
            Color tmp = spriteRenderer.color;
            tmp.a = spriteRenderer.color.a + Time.deltaTime;
            spriteRenderer.color = tmp;

            yield return null;
        }

    }
}
