using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate()
    {
        transform.position = new Vector3(
            target.position.x + offset.x,
            offset.y, 
            offset.z
        );
    }
}