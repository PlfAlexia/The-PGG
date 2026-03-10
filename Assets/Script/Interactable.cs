using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string questionKey;

    public void Interact()
    {
        UIManager.Instance.OpenQuestion(questionKey);
    }
}
