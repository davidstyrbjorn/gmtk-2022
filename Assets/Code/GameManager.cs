using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public enum GAME_STATE
{
    PAUSED,
    BAR_FIGHT,
    GAMBLING
}

public class GameManager : MonoBehaviour
{
    public GAME_STATE gameState = GAME_STATE.BAR_FIGHT;
    public AudioSource bg_barfight;
    public AudioSource bg_gamble;

    [Header("Gambling")]

    [Header("Bar Fight")]
    public Transform mapParent;
    public GameObject enemyPrefab;
    public Transform spawnPointsParent;
    private float timeBetweenSpawns = 2.0f;
    private const float MIN_SPAWN_TIME = 1.0f;
    private const float SPAWN_TIME_DECAY = 0.005f;
    private float timeSinceLastSpawn = 0.0f;

    private const float BG_CROSSFADE_SPEED = 0.5f;
    public float backgroundMusicVolume = 0.5f;

    void Start()
    {
        Application.targetFrameRate = 144;

        bg_barfight.volume = backgroundMusicVolume;
        bg_gamble.volume = backgroundMusicVolume;
        bg_barfight.Play();
        bg_gamble.Play();
        bg_barfight.loop = true;
        bg_gamble.loop = true;
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
            // timeBetweenSpawns -= SPAWN_TIME_DECAY * Time.deltaTime;
            // timeBetweenSpawns = Mathf.Max(MIN_SPAWN_TIME, timeBetweenSpawns);

            if (Input.GetKeyDown(KeyCode.G))
            {
                OnBarFightOver();
            }

            // Transition volume
            bg_barfight.volume = Mathf.MoveTowards(bg_barfight.volume, backgroundMusicVolume, BG_CROSSFADE_SPEED * Time.deltaTime);
            bg_gamble.volume = Mathf.MoveTowards(bg_gamble.volume, 0.0f, BG_CROSSFADE_SPEED * Time.deltaTime);
        }
        else if (gameState == GAME_STATE.GAMBLING)
        {
            // Whatever...

            // Transition volume
            bg_barfight.volume = Mathf.MoveTowards(bg_barfight.volume, 0.0f, BG_CROSSFADE_SPEED * Time.deltaTime);
            bg_gamble.volume = Mathf.MoveTowards(bg_gamble.volume, backgroundMusicVolume, BG_CROSSFADE_SPEED * Time.deltaTime);
        }


    }

    void SpawnEnemies()
    {
        timeSinceLastSpawn = 0.0f;
        // Pick a random spawn point
        var spawnPoints = new List<Transform>(spawnPointsParent.GetComponentsInChildren<Transform>());
        spawnPoints.RemoveAt(0);

        int index = Random.Range(0, spawnPoints.Count);
        Vector2 spawnPoint = spawnPoints[index].position;

        // Instantiate enemy at that position
        var enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        enemy.transform.SetParent(mapParent);
    }

    public void OnBarFightOver()
    {
        FindObjectOfType<SfxManager>().PlaySound("gamble_enter");
        FindObjectOfType<CanvasPhaseTransition>().SpawnGambleTransition(); // Transition effect, runs on canvas
        ToggleBarFight(false);
        FindObjectOfType<GamblingManager>().ToggleGambling(true);
        gameState = GAME_STATE.GAMBLING;
    }

    public void OnGamblingOver()
    {
        FindObjectOfType<SfxManager>().PlaySound("gamble_exit");
        FindObjectOfType<CanvasPhaseTransition>().SpawnBattleTransition(); // Transition effect, runs on canvas
        // Disable bar fight objects, set enabled to false
        ToggleBarFight(true);
        gameState = GAME_STATE.BAR_FIGHT;
    }

    private void ToggleBarFight(bool value)
    {
        var enemies = FindObjectsOfType<Enemy>();
        var hps = FindObjectsOfType<Health>();
        var playerMovement = FindObjectOfType<PlayerMovement>();
        var playerController = FindObjectOfType<PlayerController>();
        var gunBehavior = FindObjectOfType<GunBehavior>();
        var lookAtMouse = FindObjectOfType<LookAtMouse>();

        foreach (var enemy in enemies)
        {
            enemy.enabled = value;
            // Also toggle chasing & navmeshagent
            enemy.GetComponent<Chasing>().enabled = value;
            enemy.GetComponent<NavMeshAgent>().enabled = value;
        }
        foreach (var hp in hps)
        {
            hp.enabled = value;
        }
        // TODO: Maybe these should also be lists?
        playerMovement.enabled = value;
        gunBehavior.enabled = value;
        lookAtMouse.enabled = value;
    }
}
