using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Health health;
    private bool isDying = false;
    private int lastFrameHealth = 0;

    float damageTime = -100;

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

        if (lastFrameHealth > health.hp)
        {
            // We have taken damage
            sfxManager.PlaySound("player_hurt", 0.65f);
            Camera.main.GetComponent<CameraShake>().DoShake(0.1f, 0.6f);

            damageTime = Time.time;
        }

        float damageEffectSpeed = 3f;
        float epsilon = 0.5f;
        float t = Time.time - damageTime;
        if (t < (1 / damageEffectSpeed) + epsilon)
        {
            List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
            spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
            spriteRenderers.Add(GetComponent<SpriteRenderer>());

            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                SpriteRenderer spriteRenderer = spriteRenderers[i];
                float gAndB = Mathf.Lerp(0, 1, t);
                float a = Mathf.Lerp(0.3f, 1, t);


                Color newColor = new Color(1f, gAndB * damageEffectSpeed, gAndB * damageEffectSpeed, a * damageEffectSpeed);
                spriteRenderer.color = newColor;
            }
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
            GetComponent<Collider2D>().enabled = false;
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
