using UnityEngine;

public class ArmGateController : Interactable
{
    [SerializeField] private int _gateId;

    [SerializeField] private Sprite _sprite;

    private void Start()
    {
        if (GatesController.instance.gates[_gateId] == true)
        {
            GetComponent<SpriteRenderer>().sprite = _sprite;
            GetComponent<Animator>().enabled = false;

            enabled = false;
        }
    }

    public override void Interact()
    {
        base.Interact();

        GatesController.instance.gates[_gateId] = true;

        SaveManager.instance.SaveGateCondition();

        GetComponent<Animator>().SetTrigger("UseArm");
    }
}
