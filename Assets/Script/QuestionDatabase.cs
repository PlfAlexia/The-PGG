using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "Portfolio/Question Database")]
public class QuestionDatabase : ScriptableObject // on peut l'éditer directement via inspector sans toucher au code
{
    [System.Serializable]
    public class QuestionEntry
    {
        public string key;
        [TextArea] public string questionText;
    }

    public List<QuestionEntry> questions = new List<QuestionEntry>();

    // Retrouve le texte d'une question à partir de sa clé
    public string GetQuestion(string key)
    {
        QuestionEntry entry = questions.Find(q => q.key == key);
        return entry != null ? entry.questionText : "Question inconnue";
    }

    // Vérifie qu'une clé existe bien dans la base
    public bool HasKey(string key)
    {
        return questions.Exists(q => q.key == key);
    }

    // Retourne toutes les clés disponibles (utile pour générer le portfolio)
    public List<string> GetAllKeys()
    {
        List<string> keys = new List<string>();
        foreach (var entry in questions)
            keys.Add(entry.key);
        return keys;
    }
}