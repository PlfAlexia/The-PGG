using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSequence", menuName = "Portfolio/Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    public List<DialogueStep> steps = new List<DialogueStep>();
}

// Pour en faire un scriptable object