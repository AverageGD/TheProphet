using System;
using TMPro;
using UnityEngine;

public class SellingItemPickUp : ItemPickUp
{
    [SerializeField] private int price;
    [SerializeField] private GameObject itemInfoBox;
    public override void Interact()
    {
        if (price > PlayerCurrencyController.instance.currency)
            return;

        PlayerCurrencyController.instance.TakeCurrency(price);
        base.Interact();
    }

    private void Update()
    {
        if (InventoryManager.instance.Contains(item.id))
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            print("Message");

            GameObject itemInfoBoxClone = Instantiate(itemInfoBox, transform);
            itemInfoBoxClone.transform.position = transform.position;
            itemInfoBoxClone.transform.Find("SellingItemName").GetComponent<TextMeshPro>().text = item.name;
            itemInfoBoxClone.transform.Find("SellingItemPrice").GetComponent<TextMeshPro>().text = "Price: " + Convert.ToString(price);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Destroy(transform.Find("SellingItemInfo(Clone)").gameObject);
        }

    }
}
