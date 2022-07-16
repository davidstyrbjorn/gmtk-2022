using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    Rigidbody rb;
    public float fallForce = 500;
    public Vector3 dieVelocity;
    private bool isLocked = false;
    public int value = 0;

    Vector3 startPosition;
    Quaternion startRotation;
    Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        dieVelocity = rb.velocity;
        rb.AddForce(new Vector3(0, 0, fallForce * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.Space) && !isLocked)
        {
            Throw();
        }
    }

	void Throw()
	{
        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;

        rb.AddForce(dirZ, dirX, dirY);
        rb.AddTorque(dirX, dirY, dirZ);

        value = 0;
    }
    public void SetLocked(bool aIsLocked)
	{
        isLocked = aIsLocked;
        Outline outline = gameObject.GetComponent<Outline>();
		{
            outline.enabled = isLocked;
		}

        if (isLocked)
            rb.constraints = RigidbodyConstraints.FreezeAll;
        else
            rb.constraints = RigidbodyConstraints.None;
    }

    public bool GetLocked()
    {
        return isLocked;
    }
}
