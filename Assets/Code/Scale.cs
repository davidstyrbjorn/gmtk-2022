using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public float GetEnemyMovementMultiplier()
    {
        const float MULTIPLIER_PER_ROUND_PLAYED = 0.15f;
        return Mathf.Min(1.0f + MULTIPLIER_PER_ROUND_PLAYED * gameManager.timesGambled, 4);
    }

    public float GetEnemySpawnRateMultiplier()
    {
        const float MUTLIPLIER_PER_ROUND_PLAYED = 0.1f;
        return 1.5f + MUTLIPLIER_PER_ROUND_PLAYED * gameManager.timesGambled;
    }
}
