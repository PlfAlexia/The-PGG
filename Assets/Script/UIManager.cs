using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject questionPanel;
    public TMP_Text questionText;
    public TMP_InputField answerInput;

    private string currentKey;

    void Awake()
    {
        Instance = this;
    }

    public void OpenQuestion(string key)
    {
        currentKey = key;
        questionPanel.SetActive(true);

        questionText.text = DataManager.Instance.GetQuestion(key);
        answerInput.text = "";
    }

    public void ValidateAnswer()
    {
        DataManager.Instance.SaveAnswer(currentKey, answerInput.text);
        questionPanel.SetActive(false);
    }
}
