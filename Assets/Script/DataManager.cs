using UnityEngine;
using System.Collections.Generic;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance; 
    private string savePath;

    [SerializeField] private QuestionDatabase questionDatabase;


    private Dictionary<string, string> answers = new Dictionary<string, string>();

    void Awake() 
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Application.persistentDataPath + "/save.json";
        LoadData();
    }


    public List<string> GetAllQuestionKeys()
    {
        if (questionDatabase == null) return new List<string>();
        return questionDatabase.GetAllKeys();
    }

    public void SaveAnswer(string key, string value)
    {
        answers[key] = value;
        SaveToFile();
    }
    
    public string GetAnswerOrPlaceholder(string key, string placeholder = "En attente...")
    {
        return answers.ContainsKey(key) && !string.IsNullOrEmpty(answers[key]) ? answers[key] : placeholder;
    }


    public void SaveToFile()
    {
        AnswerData data = new AnswerData();

        foreach (var pair in answers)
        {
            data.keys.Add(pair.Key);
            data.values.Add(pair.Value);
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        AnswerData data = JsonUtility.FromJson<AnswerData>(json);

        answers.Clear();
        for (int i = 0; i < data.keys.Count; i++)
            answers[data.keys[i]] = data.values[i];
    }


    
}
