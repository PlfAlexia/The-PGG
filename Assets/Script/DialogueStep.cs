using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueStep
{
    public enum StepType { Normal, Choice, PortfolioInput, ImageInput }

    [TextArea] public string npcText;
    public StepType type;

    // Remplis si type == Choice
    public List<ChoiceOption> choices = new List<ChoiceOption>();

    // Remplis si type == PortfolioInput
    public string dataKey;
}

[System.Serializable]
public class ChoiceOption
{
    public string label;
    public int nextStepIndex;
}