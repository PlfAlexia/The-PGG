using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueSequence sequence;

    public void Interact()
    {
        Debug.Log("Interact appelé");
        Debug.Log("DialogueManager.Instance : " + DialogueManager.Instance);
        Debug.Log("sequence : " + sequence);

        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager introuvable !");
            return;
        }

        if (sequence == null)
        {
            Debug.LogError("Sequence non assignée sur " + gameObject.name);
            return;
        }

        DialogueManager.Instance.StartDialogue(sequence);
    }
}

// à mettre sur chaque PNJ