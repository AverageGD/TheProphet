using UnityEngine;

public class SpawnPoint : Interactable
{
    public override void Interact()
    {
        base.Interact();

        SaveManager.instance.SavePlayerData();
        GlobalFade.instance.FadeIn();
        GameManager.instance.ReloadCurrentSceneInvoker(1.5f);
    }
}
