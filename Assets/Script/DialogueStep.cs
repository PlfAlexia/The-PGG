using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueStep
{
    public enum StepType { Normal, Choice, PortfolioInput, TitleInput }

    [TextArea] public string npcText;
    public StepType type;

    // Remplis si type == Choice
    public List<string> choices = new List<string>();

    // Remplis si type == PortfolioInput
    public string dataKey;
}