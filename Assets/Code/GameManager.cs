using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public enum GAME_STATE
{
    PAUSED,
    BAR_FIGHT,
    GAMBLING
}

public class GameManager : MonoBehaviour
{
    public GAME_STATE gameState = GAME_STATE.BAR_FIGHT;

    [Header("Gambling")]

    [Header("Bar Fight")]
    public Transform mapParent;
    public GameObject enemyPrefab;
    public Transform spawnPointsParent;
    private float timeBetweenSpawns = 2.0f;
    private const float MIN_SPAWN_TIME = 1.0f;
    private const float SPAWN_TIME_DECAY = 0.005f;
    private float timeSinceLastSpawn = 0.0f;

    void Start()
    {
        Application.targetFrameRate = 144;
    }

    void Update()
    {
        // Bar fight updates
        if (gameState == GAME_STATE.BAR_FIGHT)
        {
            timeSinceLastSpawn += 1.0f * Time.deltaTime;
            if (timeSinceLastSpawn > timeBetweenSpawns)
            {
                SpawnEnemies();
            }

            // Increase the rate of spawn over time
            timeBetweenSpawns -= SPAWN_TIME_DECAY * Time.deltaTime;
            timeBetweenSpawns = Mathf.Max(MIN_SPAWN_TIME, timeBetweenSpawns);
        }
    }

    void SpawnEnemies()
    {
        timeSinceLastSpawn = 0.0f;
        // Pick a random spawn point
        var spawnPoints = new List<Transform>(spawnPointsParent.GetComponentsInChildren<Transform>());
        spawnPoints.RemoveAt(0);

        int howMany = Random.Range(1, 3);
        foreach (var _ in Enumerable.Range(1, howMany))
        {
            int index = Random.Range(0, spawnPoints.Count);
            Vector2 spawnPoint = spawnPoints[index].position;
            spawnPoints.RemoveAt(index);

            // Instantiate enemy at that position
            var enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
            enemy.transform.SetParent(mapParent);
        }
    }
}
