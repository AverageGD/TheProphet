using UnityEngine;

public class DialogueTrigger : Interactable
{
    [SerializeField] private Dialogue[] _dialogues;
    [SerializeField] private float _delayTime;
    [SerializeField] private float _maximalDistanceFromPlayer;

    public int dialogueIndex = 0;

    private float lastClickTime;
    private int indexOfCurrentSentence = 0;
    

    public override void Interact()
    {
        base.Interact();

        if (Time.time - lastClickTime > _delayTime)
        {
            if (indexOfCurrentSentence == 0)
            {
                DialogueManager.instance.StartDialogue(_dialogues[dialogueIndex]);

            } else if (indexOfCurrentSentence == _dialogues[dialogueIndex].sentences.Length)
            {
                DialogueManager.instance.EndDialogue();
                dialogueIndex++;
                dialogueIndex = Mathf.Clamp(dialogueIndex, 0, _dialogues.Length - 1);

            } else
            {
                DialogueManager.instance.DisplayNextSentence();
            }

            lastClickTime = Time.time;

            indexOfCurrentSentence++;
            indexOfCurrentSentence %= (_dialogues[dialogueIndex].sentences.Length + 1);
        }

    }

    private void OnTriggerExit2D()
    {
        indexOfCurrentSentence = 0;

        DialogueManager.instance.EndDialogue();
    }
}
