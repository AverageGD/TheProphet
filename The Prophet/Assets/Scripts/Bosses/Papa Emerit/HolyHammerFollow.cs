using UnityEngine;

public class HolyHammerFollow : MonoBehaviour
{
    private float elapsedTime = 0;

    private float currentVelocity = 0;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > 2f)
        {
            enabled = false;
        }
        if (CharacterController2D.instance != null)
            transform.position = new Vector2(Mathf.SmoothDamp(transform.position.x, CharacterController2D.instance.transform.position.x, ref currentVelocity, 0.15f),
            transform.position.y);
    }
}
