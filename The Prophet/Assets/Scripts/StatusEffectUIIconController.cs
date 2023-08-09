using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUIIconController : MonoBehaviour
{
    public static StatusEffectUIIconController instance;


    [SerializeField] private GameObject _statusEffectIconPrefab;
    [SerializeField] private List <EnemyDamage.DamageType> _damageTypes = new List<EnemyDamage.DamageType>();
    [SerializeField] private List <Sprite> _sprites = new List<Sprite>();

    private List <Transform> activeStatusEffects = new List<Transform>();
    private Dictionary <EnemyDamage.DamageType, Sprite> statusEffectIcons = new Dictionary<EnemyDamage.DamageType, Sprite>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        for (int counter = 0; counter < _damageTypes.Count; counter++)
            statusEffectIcons[_damageTypes[counter]] = _sprites[counter];
    }

    private void Update()
    {
        ShowAllActiveStatusEffects();
    }

    public void CreateNewIcon(EnemyDamage.DamageType damageType, float deleteTimer)
    {
        activeStatusEffects.Add(Instantiate(_statusEffectIconPrefab).transform);

        Destroy(activeStatusEffects[activeStatusEffects.Count - 1].gameObject, deleteTimer);

        activeStatusEffects[activeStatusEffects.Count - 1].gameObject.GetComponent<Image>().sprite = statusEffectIcons[damageType];
    }
    
    private void ShowAllActiveStatusEffects()
    {
        foreach (Transform statusEffectIcon in transform)
            Destroy(statusEffectIcon.gameObject);

        transform.DetachChildren();

        short xOffset = 0;

        foreach (Transform statusEffect in activeStatusEffects)
        {
            if (statusEffect == null) continue;

            GameObject statusEffectIconClone = Instantiate(statusEffect.gameObject, transform);
            statusEffectIconClone.transform.localPosition = new Vector2(xOffset * 100, 0);
            xOffset++;
        }
    }
}
