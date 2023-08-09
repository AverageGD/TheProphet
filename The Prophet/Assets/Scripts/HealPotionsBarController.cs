using UnityEngine;
using UnityEngine.UI;

public class HealPotionsBarController : MonoBehaviour
{
    public static HealPotionsBarController instance;

    [SerializeField] private GameObject _healPotionPrefab;
    [SerializeField] private Sprite _healPotionFullSprite;
    [SerializeField] private Sprite _healPotionEmptySprite;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void UpdateHealPotions()
    {
        foreach (Transform healPotion in transform)
            Destroy(healPotion.gameObject);

        transform.DetachChildren();

        short xOffset = 0;

        for (int counter = 1; counter <= PlayerHealthController.instance.maxHealTriesCount; counter++)
        {
            GameObject healPotionClone = Instantiate(_healPotionPrefab, transform);

            healPotionClone.transform.localPosition = new Vector2(xOffset * 100, 0);
            transform.GetChild(counter - 1).GetComponent<Image>().sprite = _healPotionFullSprite;
            xOffset++;
        }

        print(transform.childCount);
        for (int counter = transform.childCount; counter > PlayerHealthController.instance.currentHealTriesCount; counter--)
        {
            transform.GetChild(counter - 1).GetComponent<Image>().sprite = _healPotionEmptySprite;
        }
    }
}
