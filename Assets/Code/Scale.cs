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
        return 1.2f + MUTLIPLIER_PER_ROUND_PLAYED * gameManager.timesGambled;
    }

    public int GetEnemyHP()
    {
        // SLOOW scale of HP over time

        if (gameManager.timesGambled >= 24)
        {
            return 8;
        }
        if (gameManager.timesGambled >= 22)
        {
            return 7;
        }
        else if (gameManager.timesGambled >= 18)
        {
            return 6;
        }
        else if (gameManager.timesGambled >= 13)
        {
            return 5;
        }
        else if (gameManager.timesGambled >= 8)
        {
            return 4;
        }

        return 3; // Base
    }
}
