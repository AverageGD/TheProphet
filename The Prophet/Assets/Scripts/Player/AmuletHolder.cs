using UnityEngine;

public class AmuletHolder : MonoBehaviour
{
    public static AmuletHolder instance;

    public AmuletAbility firstAmuletAbility;
    public AmuletAbility secondAmuletAbility;

    private void Start()
    {
        if (instance == null)
            instance = this;

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
