// Chaque PNJ aura sa propre question

using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Dialogue dialogue;

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}