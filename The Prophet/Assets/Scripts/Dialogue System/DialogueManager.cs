using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] private GameObject _dialogueBox;

    private Queue <string> sentences;

    private Text nameText;
    private Text dialogueText;

    private void Start()
    {
        if (instance == null)
            instance = this;

        sentences = new Queue <string>();

        nameText = _dialogueBox.transform.Find("Name").gameObject.GetComponent<Text>();
        dialogueText = _dialogueBox.transform.Find("Dialogue").gameObject.GetComponent<Text>();

        _dialogueBox.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        CameraManager.instance.ChangeYOffsetInvoker(-3);
        _dialogueBox.SetActive(true);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(AddLetters(sentence));
    }

    public void EndDialogue()
    {
        CameraManager.instance.ChangeYOffsetInvoker(2);
        if (_dialogueBox != null)
            _dialogueBox.SetActive(false);
    }

    private IEnumerator AddLetters(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
