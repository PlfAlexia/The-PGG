using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text npcText;

    [Header("Zone Normal")]
    public GameObject normalZone;
    public Button nextButton;

    [Header("Zone Choix")]
    public GameObject choiceZone;
    public Button choiceButtonPrefab;
    public Transform choiceContainer;

    [Header("Zone Saisie")]
    public GameObject portfolioZone;
    public TMP_InputField portfolioInput;

    private DialogueSequence currentSequence;
    private int currentStepIndex;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        dialoguePanel.SetActive(false);
        normalZone.SetActive(false);
        choiceZone.SetActive(false);
        portfolioZone.SetActive(false);
    }

    public void StartDialogue(DialogueSequence sequence)
    {
        currentSequence = sequence;
        currentStepIndex = 0;
        dialoguePanel.SetActive(true);
        ShowStep(currentStepIndex);
    }

    private void ShowStep(int index)
    {
        if (index >= currentSequence.steps.Count)
        {
            EndDialogue();
            return;
        }

        DialogueStep step = currentSequence.steps[index];
        npcText.text = step.npcText;

        normalZone.SetActive(false);
        choiceZone.SetActive(false);
        portfolioZone.SetActive(false);

        switch (step.type)
        {
            case DialogueStep.StepType.Normal:
                normalZone.SetActive(true);
                break;
            case DialogueStep.StepType.Choice:
                ShowChoices(step.choices);
                break;
            case DialogueStep.StepType.PortfolioInput:
                ShowInputZone(step);
                break;
        }
    }

    public void OnNextButton()
    {
        currentStepIndex++;
        ShowStep(currentStepIndex);
    }

    private void ShowChoices(List<string> choices)
    {
        choiceZone.SetActive(true);

        foreach (Transform child in choiceContainer)
            Destroy(child.gameObject);

        foreach (string choice in choices)
        {
            Button btn = Instantiate(choiceButtonPrefab, choiceContainer);
            btn.GetComponentInChildren<TMP_Text>().text = choice;
            string captured = choice;
            btn.onClick.AddListener(() => OnChoiceSelected(captured));
        }
    }

    private void OnChoiceSelected(string choice)
    {
        currentStepIndex++;
        ShowStep(currentStepIndex);
    }
    private void ShowInputZone(DialogueStep step)
    {
        portfolioZone.SetActive(true);
        portfolioInput.text = DataManager.Instance.GetAnswerOrPlaceholder(step.dataKey, "");
    }

    public void OnValidatePortfolio()
    {
        DialogueStep step = currentSequence.steps[currentStepIndex];

        if (!string.IsNullOrEmpty(portfolioInput.text))
            DataManager.Instance.SaveAnswer(step.dataKey, portfolioInput.text);

        currentStepIndex++;
        ShowStep(currentStepIndex);
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentSequence = null;
    }
}