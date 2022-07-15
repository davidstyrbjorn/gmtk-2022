using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Assume map starts at y = 0 and increases with y
* and that each wall has it's sortingOrder = y
*/

public class CorrectZIndex : MonoBehaviour
{
    public bool updatePerFrame = true;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (!updatePerFrame) return;

        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y) - 1;
    }
}
