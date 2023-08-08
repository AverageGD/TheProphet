using UnityEngine;

public class SpawnPoint : Interactable
{
    public override void Interact()
    {
        base.Interact();

        SaveManager.instance.SavePlayerData();
        SaveManager.instance.SavePlayerInventory();
        SaveManager.instance.SavePlayerUpgrades();

        GlobalFade.instance.FadeIn();
        GameManager.instance.ReloadCurrentSceneInvoker(1.5f);
    }
}
