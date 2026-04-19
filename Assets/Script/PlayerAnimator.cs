using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    //private Vector2 lastMoveInput;

    // Noms des paramètres dans l'Animator Controller
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int FacingRight = Animator.StringToHash("facingRight");

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator manquant sur le Player!");
    }

    // Appelé depuis PlayerMovement.cs à chaque SetMoveInput
    public void UpdateAnimation(Vector2 moveInput)
    {
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        animator.SetBool(IsMoving, isMoving);

        // Met à jour la direction seulement si le joueur bouge
        if (moveInput.x > 0.01f)
            animator.SetBool(FacingRight, true);
        else if (moveInput.x < -0.01f)
            animator.SetBool(FacingRight, false);
        // Si x == 0 (mouvement vertical), on conserve la dernière direction
    }
}