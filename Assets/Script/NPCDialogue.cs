using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueSequence sequence;

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(sequence);
    }
}

// à mettre sur chaque PNJ