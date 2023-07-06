using TMPro;
using UnityEngine;

[System.Serializable]
public abstract class AbstractAmulet : Item
{
    public bool isWearing;

    public abstract void AbilityInvoker();

}
