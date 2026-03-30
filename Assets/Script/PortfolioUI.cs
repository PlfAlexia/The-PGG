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
    public RectTransform contentRect;

    void Start()
    {
        PortfolioPanel.SetActive(false);

        RectTransform textRect = portfolioText.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 1);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.pivot = new Vector2(0.5f, 1);
        textRect.offsetMin = new Vector2(10, 0);
        textRect.offsetMax = new Vector2(-10, -20);
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

         StartCoroutine(RefreshLayout());
    }

     private IEnumerator RefreshLayout()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        scrollRect.verticalNormalizedPosition = 1f;
    }

    public void ClosePortfolio()
    {
        PortfolioPanel.SetActive(false);
    }
}