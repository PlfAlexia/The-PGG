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
            case DialogueStep.StepType.ImageInput:
                ShowImageInput(step);
                break;
        }
    }

    public void OnNextButton()
    {
        currentStepIndex++;
        ShowStep(currentStepIndex);
    }

    private void ShowChoices(List<ChoiceOption> choices)
    {
        choiceZone.SetActive(true);

        foreach (Transform child in choiceContainer)
            Destroy(child.gameObject);

        foreach (ChoiceOption option in choices)
        {
            Button btn = Instantiate(choiceButtonPrefab, choiceContainer);
            btn.GetComponentInChildren<TMP_Text>().text = option.label;
            int capturedIndex = option.nextStepIndex;
            btn.onClick.AddListener(() => OnChoiceSelected(capturedIndex));
        }
    }

    private void OnChoiceSelected(int nextStepIndex)
    {
        currentStepIndex = nextStepIndex;
        ShowStep(currentStepIndex);
    }
    private void ShowInputZone(DialogueStep step)
    {
        portfolioZone.SetActive(true);
        portfolioInput.text = DataManager.Instance.GetAnswerOrPlaceholder(step.dataKey, "");
    }

    private void ShowImageInput(DialogueStep step)
    {
        string firstImage = DataManager.Instance.GetAnswerOrPlaceholder(
            step.dataKey + "_image_0", "");

        if (string.IsNullOrEmpty(firstImage))
            ImageManager.Instance.ResetImageIndex();

        ImageManager.Instance.OpenAndSaveImage(step.dataKey, () =>
        {
            currentStepIndex++;
            ShowStep(currentStepIndex);
        });
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