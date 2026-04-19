using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PortfolioUI : MonoBehaviour
{
    public GameObject PortfolioPanel;

    [Header ("Navigation")]
    public Button prevButton;
    public Button nextButton;
    public TMP_Text pageText;

    [Header("Section")]
    public TMP_Text titleText;
    public TMP_Text answerText;

    [Header("Police")]
    public TMP_InputField fontInput;
    public Button applyFontButton;

    private List<string> keys = new List<string>();
    private int currentIndex = 0;

    void Start()
    {
        PortfolioPanel.SetActive(false);
    }

    public void OpenPortfolio()
    {
        keys = DataManager.Instance.GetAllQuestionKeys();
        currentIndex = 0;

        fontInput.text = DataManager.Instance.GetAnswerOrPlaceholder("global_font", "");

        PortfolioPanel.SetActive(true);
        ShowSection(currentIndex);
    }

      private void ShowSection(int index)
    {
        if (keys.Count == 0) return;

        string key = keys[index];
        string titleKey = key + "_title";

        // Titre de la section — utilise titleKey si dispo, sinon la clé
        titleText.text = DataManager.Instance.GetAnswerOrPlaceholder(titleKey, key);

        answerText.text = DataManager.Instance.GetAnswerOrPlaceholder(key);

        // Pagination
        pageText.text = (index + 1) + " / " + keys.Count;

        prevButton.interactable = index > 0;
        nextButton.interactable = index < keys.Count - 1;
    }

    public void SaveFont()
    {
        if (string.IsNullOrEmpty(fontInput.text)) return;
        DataManager.Instance.SaveAnswer("global_font", fontInput.text);
        Debug.Log("Police sauvegardée : " + fontInput.text);
    }

    public void OnPrevSection()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ShowSection(currentIndex);
        }
    }

    public void OnNextSection()
    {
        if (currentIndex < keys.Count - 1)
        {
            currentIndex++;
            ShowSection(currentIndex);
        }
    }

    public void ClosePortfolio()
    {
        PortfolioPanel.SetActive(false);
    }
}