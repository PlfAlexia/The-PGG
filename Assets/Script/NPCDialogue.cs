using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueSequence sequence;

    public void Interact()
    {

        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager null !");
            return;
        }
        if (sequence == null)
        {
            Debug.LogError("Sequence null sur " + gameObject.name);
            return;
        }

        DialogueManager.Instance.StartDialogue(sequence);
    }
}

// à mettre sur chaque PNJ