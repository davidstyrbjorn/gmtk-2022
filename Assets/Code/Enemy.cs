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
    private GameManager gameManager;

    public static int pukeLayer = 10;

    void Start()
    {
        sfxManager = FindObjectOfType<SfxManager>();
        health = GetComponent<Health>();
        transform.tag = "enemy";
        lastFrameHealth = health.hp;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {

        if (lastFrameHealth > health.hp)
        {
            // We have taken damage
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
            isDying = true;
            sfxManager.PlaySound("enemy_hurt", 0.65f);
            GetComponent<Chasing>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<EnemyAnimation>().StartDie();
            GetComponent<Collider2D>().enabled = false;
            // Spawn decal at position
            if (Random.Range(0.0f, 1.0f) > 0.5f) return;
            GameObject decal = (GameObject)Instantiate(gameManager.pukePrefab, transform.position, Quaternion.identity);
            decal.GetComponent<SpriteRenderer>().color
                = new Vector4(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1), 1);
            decal.GetComponent<SpriteRenderer>().sortingOrder = Enemy.pukeLayer;
            Enemy.pukeLayer++;
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
