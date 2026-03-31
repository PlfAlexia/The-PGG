using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //ajoute rigid body si j'oublie
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerAnimator playerAnimator;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    // Cette fonction est appelée depuis Player.cs
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
        playerAnimator?.UpdateAnimation(input);
    }

    void FixedUpdate()
    {
        // Déplace le joueur avec linearVelocity
        rb.linearVelocity = moveInput * moveSpeed;
    }
}