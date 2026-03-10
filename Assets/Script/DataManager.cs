using UnityEngine;
using System.Collections.Generic;
using System.IO;


// ATTENTION pour l'instant sauvegarde automatique -> potentiellement faire un bouton sauvegarde ou autre

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private string savePath;

    Dictionary<string, string> answers = new Dictionary<string, string>();

    void Awake()
    {
        Instance = this;
        savePath = Application.persistentDataPath + "/save.json";
        LoadData();
    }

    public string GetQuestion(string key)
    {
        switch(key)
        {
            case "bio": return "Présente-toi en quelques mots";
            case "skills": return "Quelles sont tes compétences ?";
            case "project": return "Décris un de tes projets";
            default: return "Question inconnue";
        }
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
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            AnswerData data = JsonUtility.FromJson<AnswerData>(json);

            answers.Clear();

            for (int i = 0; i < data.keys.Count; i++)
            {
                answers[data.keys[i]] = data.values[i];
            }
        }
    }


    public void SaveAnswer(string key, string value)
    {
        answers[key] = value;
        SaveToFile(); // sauvegarde automatique
    }

    /*public string GetAnswer(string key)
    {
        if (answers.ContainsKey(key))
            return answers[key];

        return "Aucune réponse encore...";
    }*/
    public string GetAnswerOrPlaceholder(string key, string placeholder = "En attente...")
    {
        return answers.ContainsKey(key) && !string.IsNullOrEmpty(answers[key]) ? answers[key] : placeholder;
    }
}
