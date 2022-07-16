using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float interpVelocity;
    public Transform target;
    public Vector2 offset;
    Vector3 targetPos;
    float originalZ;

    void Start()
    {
        originalZ = transform.position.z;
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        if (!target) return;

        // Calculate offset
        var mousePos = (Vector2)Input.mousePosition;
        mousePos.x -= Screen.width / 2; mousePos.x /= Screen.width;
        mousePos.y -= Screen.height / 2; mousePos.y /= Screen.height;
        var relativeOffset = new Vector3();
        // if (Vector2.SqrMagnitude(mousePos) >= 0.05f)
        // {
        relativeOffset = (Vector3)(mousePos * offset);
        // }

        // Calculate new position
        Vector3 newPosition = Vector2.Lerp(transform.position, target.position, interpVelocity * Time.fixedDeltaTime);
        transform.position = relativeOffset + newPosition + Vector3.forward * originalZ;
    }
}