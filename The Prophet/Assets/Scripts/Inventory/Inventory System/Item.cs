using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject // Item scriptable object for the visual connection to the UI
{
    public int id;
    public string itemName;
    public Sprite icon;
    public ItemType itemType;

    public RibbonAbility ribbonAbility;

    public enum ItemType
    {
        ribbon,
        amulet,
        keyItem
    }
}
