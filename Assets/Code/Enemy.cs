using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Health health;
    private bool isDying = false;
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
        if (health.hp <= 0 && !isDying)
        {
            // We have taken damage
            isDying = true;
            sfxManager.PlaySound("enemy_hurt", 0.65f);
            GetComponent<Chasing>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<EnemyAnimation>().StartDie();
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
