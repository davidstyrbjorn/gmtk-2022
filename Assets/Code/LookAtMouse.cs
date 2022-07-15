using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer playerSpriteRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        //  Rotation logic from Nade: https://youtu.be/6hp9-mslbzI

        Vector2 anchorPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = Input.mousePosition;

        Vector2 difference = mousePosition - anchorPosition;
        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        //  Flip playersprite and gun if the mouse is on the left side of the player.
        if(rotationZ > 90.0f || rotationZ < -90.0f)
        {
            transform.localRotation = Quaternion.Euler(180.0f, 0, -rotationZ);
            playerSpriteRenderer.flipX = true;
        }
        else if (playerSpriteRenderer)
        {
            playerSpriteRenderer.flipX = false;
        }
    }
}
