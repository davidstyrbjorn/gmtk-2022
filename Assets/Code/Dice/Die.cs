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

    float stuckTime = 0f;
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

        if (!IsMoving())
		{
            stuckTime += Time.fixedDeltaTime;

            if (stuckTime > 1f)
            {
                if (!HasValue())
                    Throw();
            }
        }
        else
		{
            stuckTime = 0;
		}
    }
	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.tag == "dicewall")
		{
            rb.AddForce(collision.gameObject.transform.up * 20);
		}
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

        stuckTime = 0f;
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
            outline.OutlineColor = Color.red;
            outline.OutlineWidth = 8;
        }

        if (isLocked)
		{
            rb.constraints = RigidbodyConstraints.FreezeAll;
            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
            boxCollider.isTrigger = true;
        }
        else
		{
            rb.constraints = RigidbodyConstraints.None;

            BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
            boxCollider.isTrigger = false;
        }
    }

    public bool GetLocked()
    {
        return isLocked;
    }

	private void OnMouseOver()
	{
        if (!isLocked)
        {
            Outline outline = gameObject.GetComponent<Outline>();
            outline.enabled = true;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 4;
        }
    }

    private void OnMouseExit()
    {
        if (!isLocked)
        {
            Outline outline = gameObject.GetComponent<Outline>();
            outline.enabled = false;
        }
    }

	void OnMouseDown()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        if (rigidBody != null)
        {
            if (rigidBody.gameObject.TryGetComponent(out Die die))
            {
                if (!die.IsMoving() && die.HasValue())
                    die.SetLocked(!die.GetLocked());
            }
        }
    }
}
