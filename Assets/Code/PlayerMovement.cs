using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private Vector2 moveSpeedVector;
    private Rigidbody2D rBody;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        moveSpeedVector = new Vector2(moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        velocity = new Vector2(inputX, inputY);
        velocity = velocity.normalized * moveSpeedVector;
    }

    void FixedUpdate()
    {
        rBody.MovePosition(rBody.position + velocity * Time.fixedDeltaTime);
    }
}
