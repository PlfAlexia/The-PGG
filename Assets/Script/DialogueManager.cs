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

    // Zone Normal
    public GameObject normalZone;
    public Button nextButton;

    // Zone Choix
    public GameObject choiceZone;
    public Button choiceButtonPrefab;
    public Transform choiceContainer;

    // Zone Portfolio
    public GameObject portfolioZone;
    public TMP_InputField portfolioInput;
    public TMP_Text portfolioQuestion;

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
                ShowPortfolioInput(step);
                break;
            case DialogueStep.StepType.TitleInput:
                ShowTitleInput(step);
                break;
        }
    }

    // --- Normal ---
    public void OnNextButton()
    {
        currentStepIndex++;
        ShowStep(currentStepIndex);
    }

    // --- Choix ---
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

    // --- Portfolio ---
    private void ShowPortfolioInput(DialogueStep step)
    {
        portfolioZone.SetActive(true);
        portfolioQuestion.text = DataManager.Instance.GetQuestion(step.dataKey);
        portfolioInput.text = DataManager.Instance.GetAnswerOrPlaceholder(step.dataKey, "");
    }

    // --- Titre ---
    private void ShowTitleInput(DialogueStep step)
    {
        portfolioZone.SetActive(true);
        portfolioQuestion.text = step.npcText;
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