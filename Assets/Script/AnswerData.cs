using System.Collections.Generic;

[System.Serializable] //pour que Unity convertisse en JSON
public class AnswerData // pour contrer le Dictionary pas sérialisable par JsonUtility
{
    public List<string> keys = new List<string>();
    public List<string> values = new List<string>();
}

// Contient les données et défini la structure des données