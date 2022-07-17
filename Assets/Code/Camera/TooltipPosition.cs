using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipPosition : MonoBehaviour
{
    public Vector3 offset;
    void Update()
    {
        Vector2 ps = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = ps + (Vector2)offset;
    }
}
