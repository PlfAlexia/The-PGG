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

    // Zone Normal — juste un bouton Suivant
    public GameObject normalZone;
    public Button nextButton;

    // Zone Choix — boutons générés dynamiquement
    public GameObject choiceZone;
    public Button choiceButtonPrefab;
    public Transform choiceContainer;

    // Zone Portfolio — champ de saisie
    public GameObject portfolioZone;
    public TMP_InputField portfolioInput;
    public TMP_Text portfolioQuestion;
    public TMP_InputField titleInput;
    public TMP_Text titleQuestion;

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

    // Appelé par NPCDialogue
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

        // Cache toutes les zones puis affiche la bonne
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

        // Nettoie les anciens boutons
        foreach (Transform child in choiceContainer)
            Destroy(child.gameObject);

        // Génère un bouton par choix
        foreach (string choice in choices)
        {
            Button btn = Instantiate(choiceButtonPrefab, choiceContainer);
            btn.GetComponentInChildren<TMP_Text>().text = choice;

            // Capture la variable pour le lambda
            string captured = choice;
            btn.onClick.AddListener(() => OnChoiceSelected(captured));
        }
    }

    private void OnChoiceSelected(string choice)
    {
        // Pas sauvegardé — juste roleplay
        currentStepIndex++;
        ShowStep(currentStepIndex);
    }

    // --- Portfolio ---
    private void ShowPortfolioInput(DialogueStep step)
    {
        portfolioZone.SetActive(true);
        portfolioQuestion.text = DataManager.Instance.GetQuestion(step.dataKey);
        portfolioInput.text = DataManager.Instance.GetAnswerOrPlaceholder(step.dataKey, "");

        // Affiche la question titre si définie
        bool hasTitleKey = !string.IsNullOrEmpty(step.titleKey);
        titleInput.gameObject.SetActive(hasTitleKey);
        titleQuestion.gameObject.SetActive(hasTitleKey);

        if (hasTitleKey)
            titleQuestion.text = "Comment appeler cette section ?";
    }

    public void OnValidatePortfolio()
    {
        DialogueStep step = currentSequence.steps[currentStepIndex];

        if (!string.IsNullOrEmpty(portfolioInput.text))
            DataManager.Instance.SaveAnswer(step.dataKey, portfolioInput.text);

        if (!string.IsNullOrEmpty(step.titleKey) && !string.IsNullOrEmpty(titleInput.text))
            DataManager.Instance.SaveAnswer(step.titleKey, titleInput.text);

        currentStepIndex++;
        ShowStep(currentStepIndex);
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentSequence = null;
    }
}