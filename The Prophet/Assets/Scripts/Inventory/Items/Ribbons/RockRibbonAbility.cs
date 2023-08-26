using UnityEngine;

[CreateAssetMenu]
public class RockRibbonAbility : RibbonAbility
{
    public override void Activate()
    {
        base.Activate();

        Debug.Log("Rock Ribbon has been called");

        GameObject obj;

        Destroy(obj = Instantiate(abilityObject), 1.1f);

        obj.transform.position = CharacterController2D.instance.transform.Find("AttackPoint").position;
        obj.GetComponent<AbilityDamageController>().damage = damage;

        VibrationController.instance.StartVibration(0.5f, 0.5f, 0.3f);
        CinemachineShake.instance.Shake(2.5f, 0.3f);

        if (CharacterController2D.instance.gameObject.GetComponent<SpriteRenderer>().flipX)
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        else
            obj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }
}
