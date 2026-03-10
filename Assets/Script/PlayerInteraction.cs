using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction")]
    public float interactRange = 1f;
    public LayerMask interactLayer;

    public void Interact()
    {
        // Raycast simple devant le joueur
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, interactRange, interactLayer);
        if (hit.collider != null)
        {
            Debug.Log("Interaction avec : " + hit.collider.name);
            // Ici tu peux appeler une méthode sur l'objet hit
        }
        else
        {
            Debug.Log("Rien à interagir !");
        }
    }
}