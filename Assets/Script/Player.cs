using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Move.IPlayerActions
{
    private Move controls;
    private PlayerMovement movement;
    private PortfolioUI portfolioUI;

    // Pour l'interaction avec les PNJ
    private NPCDialogue currentNPC;

    void Awake()
    {
        controls = new Move();

        movement = GetComponent<PlayerMovement>();
        if (movement == null)
            Debug.LogError("PlayerMovement manquant sur le Player!");

        portfolioUI = FindFirstObjectByType<PortfolioUI>();

        controls.Player.SetCallbacks(this); // à comprendre
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    // --- Déplacement ---
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        if (movement != null)
            movement.SetMoveInput(input);
    }

    // Vérifie si un champ texte est actif 
    private bool IsTypingInInputField()
    {
        var selected = EventSystem.current.currentSelectedGameObject;
        if (selected == null) return false;

        TMP_InputField input = selected.GetComponent<TMP_InputField>();
        return input != null && input.isFocused;
    }
   // --- Portfolio ---
    public void OnOpenPortfolio(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (IsTypingInInputField()) return; // plus de duplication

        if (portfolioUI != null && !portfolioUI.PortfolioPanel.activeSelf)
            portfolioUI.OpenPortfolio();
    }

    // --- Interaction PNJ ---
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (IsTypingInInputField()) return;
        if (currentNPC == null) return;
        
        currentNPC.Interact();
    }

    // --- Détection des PNJ proches ---
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger détecté : " + other.gameObject.name + " tag : " + other.tag);
        if (other.CompareTag("NPC"))
            currentNPC = other.GetComponent<NPCDialogue>();
        
            //Debug.Log("NPC assigné : " + currentNPC);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC") && other.GetComponent<NPCDialogue>() == currentNPC)
            currentNPC = null;
    }
}