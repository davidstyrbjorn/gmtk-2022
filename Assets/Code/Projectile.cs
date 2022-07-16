using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector2 direction;
    private Vector2 spawnPosition;
    private Vector2 targetPosition;
    private Vector3 velocity;

    [SerializeField] private float projectileSpeed = 10f;
 
    // Start is called before the first frame update
    void Start()
    {
        velocity = direction * projectileSpeed;
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
}
