using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health health;
    private int lastFrameHealth = 0;
    SfxManager sfxManager;

    void Start()
    {
        sfxManager = FindObjectOfType<SfxManager>();
        health = GetComponent<Health>();
        transform.tag = "enemy";
        lastFrameHealth = health.hp;
    }

    void Update()
    {
        if (lastFrameHealth != health.hp)
        {
            // We have taken
        }

        // Check for death
        if (health.hp <= 0)
        {
            // We have taken damage
            sfxManager.PlaySound("enemy_hurt", 0.65f);
            Destroy(gameObject);
        }

        lastFrameHealth = health.hp;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            // Get health component, decrease health
            if (other.TryGetComponent(out Health player_health))
            {
                player_health.TakeDamage(1);
            }
        }
    }
}
