using UnityEngine;

public class AmuletHolder : MonoBehaviour
{
    public static AmuletHolder instance;

    public AmuletAbility firstAmuletAbility;
    public AmuletAbility secondAmuletAbility;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (firstAmuletAbility != null)
        {
            firstAmuletAbility.Activate();
        }

        if (secondAmuletAbility != null)
        {
            secondAmuletAbility.Activate();
        }
    }
}
