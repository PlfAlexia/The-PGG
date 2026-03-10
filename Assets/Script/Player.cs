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
    private Interactable currentInteractable;

    void Awake()
    {
        controls = new Move();

        movement = GetComponent<PlayerMovement>();
        if (movement == null)
            Debug.LogError("PlayerMovement manquant sur le Player!");

        portfolioUI = FindFirstObjectByType<PortfolioUI>();

        controls.Player.SetCallbacks(this);
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

    // --- Portfolio ---
    public void OnOpenPortfolio(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        // Si on est en train d'écrire dans un champ texte
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            TMP_InputField input = EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>();

            if (input != null && input.isFocused)
                return;
        }

        // Si le portfolio existe + s'il existe ne pas ouvrir à nouveau
        if (portfolioUI != null && !portfolioUI.PortfolioPanel.activeSelf)
        {
            portfolioUI.OpenPortfolio();
        }
    }
    // --- Interaction PNJ ---
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    // --- Détection des PNJ proches ---
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
            currentInteractable = other.GetComponent<Interactable>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC") && other.GetComponent<Interactable>() == currentInteractable)
            currentInteractable = null;
    }
}