using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Health health;
    private int lastFrameHealth = 0;

    void Start()
    {
        health = GetComponent<Health>();
        lastFrameHealth = health.hp;
    }

    void Update()
    {
        if (lastFrameHealth != health.hp)
        {
            Camera.main.GetComponent<CameraShake>().DoShake(0.2f, 0.5f);
        }

        if (health.hp <= 0)
        {
            // Game over!
            print("GAME OVER!!!");
            Destroy(gameObject);
        }

        lastFrameHealth = health.hp;
    }
}
