using System.Collections;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    public static StatusEffectManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void GivePlayerCorrespondingStatusEffect(EnemyDamage.DamageType damageType)
    {
        switch (damageType)
        {
            case EnemyDamage.DamageType.poison:
                StartCoroutine(PoisonPlayer());
                StatusEffectUIIconController.instance.CreateNewIcon(damageType, 10);
                break;

            case EnemyDamage.DamageType.clear:
                break;

            default:
                break;
        }
    }

    private IEnumerator PoisonPlayer()
    {
        for (int repeatCount = 0; repeatCount < 20; repeatCount++)
        {
            if (PlayerHealthController.instance != null)
                PlayerHealthController.instance.TakeDamage(0.2f, Vector2.zero, false);

            yield return new WaitForSeconds(0.5f);
        }
    }
}
