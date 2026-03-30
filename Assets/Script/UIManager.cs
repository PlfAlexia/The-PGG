using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panel Question")]
    public GameObject questionPanel;
    public TMP_Text questionText;
    public TMP_InputField answerInput;

    private string currentKey;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void OpenQuestion(string key)
    {
        // Vérifie que la clé existe dans la QuestionDatabase
        if (!DataManager.Instance.QuestionExists(key))
        {
            Debug.LogWarning("Clé inconnue : " + key);
            return;
        }

        currentKey = key;
        questionPanel.SetActive(true);
        questionText.text = DataManager.Instance.GetQuestion(key);

        // Pré-remplit avec la réponse existante si le joueur a déjà répondu
        answerInput.text = DataManager.Instance.GetAnswerOrPlaceholder(key, "");
    }

    public void ValidateAnswer()
    {
        // N'enregistre pas si le champ est vide
        if (string.IsNullOrEmpty(answerInput.text))
        {
            Debug.LogWarning("Réponse vide, non sauvegardée.");
            return;
        }

        DataManager.Instance.SaveAnswer(currentKey, answerInput.text);
        questionPanel.SetActive(false);
    }

    // Brancher sur un bouton "Fermer" si tu veux permettre d'annuler
    public void ClosePanel()
    {
        questionPanel.SetActive(false);
        answerInput.text = "";
        currentKey = "";
    }
}