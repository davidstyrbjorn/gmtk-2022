using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Health health;
    private int lastFrameHealth = 0;

    private SfxManager sfxManager;

    void Start()
    {
        health = GetComponent<Health>();
        lastFrameHealth = health.hp;
        sfxManager = FindObjectOfType<SfxManager>();
    }

    void Update()
    {
        if (lastFrameHealth != health.hp)
        {
            // We have taken damage
            sfxManager.PlaySound("player_hurt", 0.65f);
            Camera.main.GetComponent<CameraShake>().DoShake(0.1f, 0.6f);
        }

        if (health.hp <= 0)
        {
            // Game over!
            Destroy(gameObject);
        }

        lastFrameHealth = health.hp;
    }
}
