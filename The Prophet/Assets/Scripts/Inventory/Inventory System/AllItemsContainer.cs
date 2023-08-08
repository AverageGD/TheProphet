using System.Collections.Generic;
using UnityEngine;

public class AllItemsContainer : MonoBehaviour
{
    public static AllItemsContainer instance;

    [SerializeField] private List<Item> items = new List<Item>();

    public Dictionary<int, Item> itemsDictionary = new Dictionary<int, Item>();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        foreach (Item item in items)
        {
            itemsDictionary[item.id] = item;
        }
    }
}
