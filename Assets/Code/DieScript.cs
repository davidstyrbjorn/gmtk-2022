using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    static Rigidbody rb;
    public float force = 1000;
    public float fallForce = 10;
    public static Vector3 diceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;

        rb.AddForce(new Vector3(0, -fallForce, 0));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            transform.position = new Vector3(0, 2, 0);
            transform.rotation = Quaternion.identity;
            rb.AddForce(transform.up * force);
            rb.AddTorque(dirX, dirY, dirZ);
        }
    }
}
