using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "Portfolio/Question Database")]
public class QuestionDatabase : ScriptableObject // on peut l'éditer directement via inspector sans toucher au code
{
    [System.Serializable]
    public class QuestionEntry
    {
        public string key;
    }

    public List<QuestionEntry> questions = new List<QuestionEntry>();


    // Retourne toutes les clés disponibles (utile pour générer le portfolio)
    public List<string> GetAllKeys()
    {
        List<string> keys = new List<string>();
        foreach (var entry in questions)
            keys.Add(entry.key);
        return keys;
    }
}