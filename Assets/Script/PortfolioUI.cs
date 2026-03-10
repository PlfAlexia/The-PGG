using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PortfolioUI : MonoBehaviour
{
    public GameObject PortfolioPanel;

    public TMP_Text bioText;
    public TMP_Text skillsText;
    public TMP_Text projectText;

    void Start()
    {
        PortfolioPanel.SetActive(false);
    }

    public void OpenPortfolio()
    {
        bioText.text = "Bio : " + DataManager.Instance.GetAnswerOrPlaceholder("bio");
        skillsText.text = "Skills : " + DataManager.Instance.GetAnswerOrPlaceholder("skills");
        projectText.text = "Project : " + DataManager.Instance.GetAnswerOrPlaceholder("project");

        PortfolioPanel.SetActive(true);
    }

    public void ClosePortfolio()
    {
        PortfolioPanel.SetActive(false);
    }
}