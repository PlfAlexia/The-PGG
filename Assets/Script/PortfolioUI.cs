using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PortfolioUI : MonoBehaviour
{
    public GameObject PortfolioPanel;

    // Un seul champ texte qui affiche tout le portfolio
    public TMP_Text portfolioText;
    public ScrollRect scrollRect; // remonte en haut à l'ouverture

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

        // Attend la fin du frame avant de scroller
        StartCoroutine(ScrollToTop());
    }

    private IEnumerator ScrollToTop()
    {
        // Laisse Unity recalculer le layout avant de scroller
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void ClosePortfolio()
    {
        PortfolioPanel.SetActive(false);
    }
}