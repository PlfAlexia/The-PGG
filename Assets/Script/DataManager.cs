using UnityEngine;
using System.Collections.Generic;
using System.IO;


// ATTENTION pour l'instant sauvegarde automatique -> potentiellement faire un bouton sauvegarde ou autre

public class DataManager : MonoBehaviour
{
    public static DataManager Instance; //instance pour rendre accessible n'importe où dans le jeu
    private string savePath;

    [SerializeField] private QuestionDatabase questionDatabase;


    Dictionary<string, string> answers = new Dictionary<string, string>();

    void Awake() //en commentaire c'est pour rendre plus robuste
    {
        // if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        // DontDestroyOnLoad(gameObject);
        savePath = Application.persistentDataPath + "/save.json";
        Debug.Log("Save path : " + savePath);
        LoadData();
    }

    // Délègue simplement au ScriptableObject
    public string GetQuestion(string key)
    {
        return questionDatabase.GetQuestion(key);
    }

    public bool QuestionExists(string key)
    {
        return questionDatabase.HasKey(key);
    }

    public List<string> GetAllQuestionKeys()
    {
        return questionDatabase.GetAllKeys();
    }

    public void SaveAnswer(string key, string value)
    {
        Debug.Log("SaveAnswer : " + key + " = " + value);
        answers[key] = value;
        SaveToFile(); // sauvegarde automatique
    }

    /*public string GetAnswer(string key)
    {
        if (answers.ContainsKey(key))
            return answers[key];

        return "Aucune réponse encore...";
    }*/
    
    // vérifier si c'est bien en place 
    public string GetAnswerOrPlaceholder(string key, string placeholder = "En attente...")
    {
        Debug.Log("GetAnswerOrPlaceholder — key : " + key + " | contient : " + answers.ContainsKey(key));
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


    
}
