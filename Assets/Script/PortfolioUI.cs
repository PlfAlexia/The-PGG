using UnityEngine;
using TMPro;

public class PortfolioUI : MonoBehaviour
{
    public GameObject PortfolioPanel;

    // Un seul champ texte qui affiche tout le portfolio
    public TMP_Text portfolioText;

    void Start()
    {
        PortfolioPanel.SetActive(false);
    }

    public void OpenPortfolio()
    {
        string content = "";

        // Parcourt toutes les clés de la QuestionDatabase
        foreach (string key in DataManager.Instance.GetAllQuestionKeys())
        {
            string question = DataManager.Instance.GetQuestion(key);
            string answer = DataManager.Instance.GetAnswerOrPlaceholder(key);

            content += $"<b>{question}</b>\n{answer}\n\n";
        }

        portfolioText.text = content;
        PortfolioPanel.SetActive(true);
    }

    public void ClosePortfolio()
    {
        PortfolioPanel.SetActive(false);
    }
}