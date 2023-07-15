using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject // Item scriptable object for the visual connection to the UI and saving in InventoryManager script
{
    public int id;
    public string itemName;

    [TextArea(3, 10)]
    public string description;

    public Sprite icon;
    public ItemType itemType;

    public RibbonAbility ribbonAbility;
    public AmuletAbility amuletAbility;

    public enum ItemType
    {
        ribbon,
        amulet,
        keyItem
    }
}
