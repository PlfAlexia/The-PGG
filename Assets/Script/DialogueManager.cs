using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public TMP_Text npcText;
    public TMP_InputField inputField;

    private string currentKey;

    void Awake()
    {
        Instance = this;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialoguePanel.SetActive(true);

        npcText.text = dialogue.npcText;

        if (dialogue.requireInput)
        {
            inputField.gameObject.SetActive(true);
            currentKey = dialogue.dataKey;
        }
        else
        {
            inputField.gameObject.SetActive(false);
        }
    }

    public void SubmitInput()
    {
        string answer = inputField.text;

        DataManager.Instance.SaveAnswer(currentKey, answer);

        dialoguePanel.SetActive(false);
        inputField.text = "";
    }
}