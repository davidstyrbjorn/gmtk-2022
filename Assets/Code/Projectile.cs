using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 spawnPosition;
    private Vector2 targetPosition;
    private Vector3 velocity;

    [SerializeField] private float lifetime = 5.0f;
    [SerializeField] private float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        velocity = direction * projectileSpeed;
        StartCoroutine(DestroySelfTimer());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += velocity * Time.fixedDeltaTime;
    }

    public void SetDirection(Vector2 inDirection)
    {
        direction = inDirection;
    }

    IEnumerator DestroySelfTimer()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with enemy
        if (other.transform.CompareTag("enemy"))
        {
            // Grab health component
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(1);
            }

            // Remove this projectile
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if(!collision.collider.transform.CompareTag("Player") && !collision.collider.transform.CompareTag("projectile"))
            Destroy(gameObject);
    }

}
