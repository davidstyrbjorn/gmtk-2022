using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    Rigidbody rb;
    public float fallForce = 500;
    public float getLooseForce = 20;

    private bool isLocked = false;
    public int value = 0;

    Vector3 startPosition;
    Quaternion startRotation;
    Vector3 startScale;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startPosition = transform.localPosition;
        startRotation = transform.rotation;
        startScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }
        Vector3 force = new Vector3(0, 0, fallForce);

        rb.AddForce(force);
    }

    public void Throw()
    {
        if (!rb) rb = GetComponent<Rigidbody>();

        float dirX = Random.Range(0, 500);
        float dirY = Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.localPosition = startPosition;
        transform.rotation = startRotation;
        transform.localScale = startScale;

        rb.AddForce(dirZ, dirX, dirY);
        rb.AddTorque(dirX, dirY, dirZ);

        value = 0;
    }
    public bool HasValue()
    {
        return value > 0;
    }

    public bool IsMoving()
    {
        float rotEpsilon = 0.1f;
        bool rotationSettled = rb.angularVelocity.magnitude > rotEpsilon;

        float velEpsilon = 0.1f;
        bool positionSettled = rb.velocity.magnitude > velEpsilon;

        return rotationSettled && positionSettled;
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

    void OnMouseDown()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            if (rigidBody.gameObject.TryGetComponent(out Die die))
            {
                if (!die.IsMoving())
                    die.SetLocked(!die.GetLocked());
            }
        }
    }
}
