using UnityEngine;

public class LeperSwordDamage : EnemyDamage
{
    [SerializeField] private float _moveTime;
    [SerializeField] private float _speed;

    private float currentTime = 0;
    private bool isThrown;
    private short direction;
    private void Start()
    {
        isThrown = true;
        direction = 1;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= _moveTime)
        {
            if (isThrown)
            {
                currentTime = 0;
                direction = -1;
                isThrown = false;
            } else
            {
                Destroy(gameObject);
            }
        }

        transform.Translate(_speed * Time.deltaTime * transform.right);

    }
}
