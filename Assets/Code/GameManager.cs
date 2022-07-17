using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using TMPro;

public enum GAME_STATE
{
    PAUSED,
    BAR_FIGHT,
    GAMBLING
}



public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnPoint
    {
        public SECTION section;
        public Transform transform;
    }

    [System.Serializable]
    public enum SECTION
    {
        BAR,
        KITCHEN,
        GAME
    }

    public GAME_STATE gameState = GAME_STATE.BAR_FIGHT;
    public AudioSource bg_barfight;
    public AudioSource bg_gamble;
    [SerializeField] private Texture2D crosshairCursor;
    [SerializeField] private Texture2D normalCursor;

    [Header("Gambling")]
    public int timesGambled = 0;
    public TextMeshProUGUI debugText;

    [Header("Bar Fight")]
    public Transform mapParent;
    public GameObject enemyPrefab;
    [SerializeField] public SpawnPoint[] spawnPoints;
    private float timeAtEnter;
    private int barFightDuration = 30;
    private float timeBetweenSpawns = 2.0f;
    private const float MIN_SPAWN_TIME = 1.0f;
    private const float SPAWN_TIME_DECAY = 0.005f;
    private float timeSinceLastSpawn = 0.0f;
    private Scale scale;

    private const float BG_CROSSFADE_SPEED = 0.5f;
    public float backgroundMusicVolume = 0.5f;

    public TMPro.TextMeshProUGUI countdownTimer;
    private PlayerController playerController;

    void Start()
    {
        Application.targetFrameRate = 144;

        scale = FindObjectOfType<Scale>();

        // Background music seutp
        Cursor.SetCursor(crosshairCursor, new Vector2(crosshairCursor.width / 2, crosshairCursor.height / 2), CursorMode.Auto);

        playerController = FindObjectOfType<PlayerController>();

        bg_barfight.volume = backgroundMusicVolume;
        bg_gamble.volume = backgroundMusicVolume;
        bg_barfight.Play();
        bg_gamble.Play();
        bg_barfight.loop = true;
        bg_gamble.loop = true;
    }

    void Update()
    {
        string text = " Barfight number: " + (timesGambled + 1).ToString();
        text += "\n EnemySpawnRateMultiplier: " + scale.GetEnemySpawnRateMultiplier();
        text += "\n EnemyMoveSpeedMultiplier: " + scale.GetEnemyMovementMultiplier();
        debugText.SetText(text);
        // Bar fight updates
        if (gameState == GAME_STATE.BAR_FIGHT)
        {
            timeSinceLastSpawn += (scale.GetEnemySpawnRateMultiplier() * Time.deltaTime);
            if (timeSinceLastSpawn > timeBetweenSpawns)
            {
                SpawnEnemies();
            }

            // Check if we spent enough time in the bar
            float timeSpentFighting = Time.timeSinceLevelLoad - timeAtEnter;
            if (timeSpentFighting > barFightDuration)
            {
                OnBarFightOver();
            }

            // Countdown UI
            int time = barFightDuration - Mathf.RoundToInt(timeSpentFighting);
            countdownTimer.SetText("NEED TO GAMBLE: " + time);

            // Transition volume
            bg_barfight.volume = Mathf.MoveTowards(bg_barfight.volume, backgroundMusicVolume, BG_CROSSFADE_SPEED * Time.deltaTime);
            bg_gamble.volume = Mathf.MoveTowards(bg_gamble.volume, 0.0f, BG_CROSSFADE_SPEED * Time.deltaTime);
        }
        else if (gameState == GAME_STATE.GAMBLING)
        {
            FindObjectOfType<PlayerController>().GetComponent<Animator>().SetBool("isRunning", false);
            // Transition volume
            bg_barfight.volume = Mathf.MoveTowards(bg_barfight.volume, 0.0f, BG_CROSSFADE_SPEED * Time.deltaTime);
            bg_gamble.volume = Mathf.MoveTowards(bg_gamble.volume, backgroundMusicVolume, BG_CROSSFADE_SPEED * Time.deltaTime);
        }
    }

    void SpawnEnemies()
    {
        timeSinceLastSpawn = 0.0f;

        // Filter and pick spawn point
        var filtered = spawnPoints.ToList().FindAll(sp => sp.section != playerController.currentSection);
        int index = Random.Range(0, filtered.Count);
        Vector2 spawnPoint = filtered[index].transform.position;

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
        timesGambled++;
        gameState = GAME_STATE.GAMBLING;
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnGamblingOver()
    {
        FindObjectOfType<SfxManager>().PlaySound("gamble_exit");
        FindObjectOfType<CanvasPhaseTransition>().SpawnBattleTransition(); // Transition effect, runs on canvas
        // Disable bar fight objects, set enabled to false
        ToggleBarFight(true);
        gameState = GAME_STATE.BAR_FIGHT;
        Cursor.SetCursor(crosshairCursor, new Vector2(crosshairCursor.width / 2, crosshairCursor.height / 2), CursorMode.Auto);
        timeAtEnter = Time.timeSinceLevelLoad;
    }

    private void ToggleBarFight(bool value)
    {
        var enemies = FindObjectsOfType<Enemy>();
        var hps = FindObjectsOfType<Health>();
        var playerMovement = FindObjectOfType<PlayerMovement>();
        var playerController = FindObjectOfType<PlayerController>();
        var gunBehavior = FindObjectOfType<GunBehavior>();
        var lookAtMouse = FindObjectOfType<LookAtMouse>();
        var cameraFollow = FindObjectOfType<CameraFollow>();

        foreach (var enemy in enemies)
        {
            enemy.enabled = value;
            // Also toggle chasing & navmeshagent
            enemy.GetComponent<Chasing>().enabled = value;
            enemy.GetComponent<NavMeshAgent>().enabled = value;
            enemy.GetComponent<Collider2D>().enabled = value;
        }
        foreach (var hp in hps)
        {
            hp.enabled = value;
        }
        // TODO: Maybe these should also be lists?
        playerMovement.enabled = value;
        playerMovement.gameObject.GetComponentInChildren<Light>().enabled = value;
        gunBehavior.enabled = value;
        lookAtMouse.enabled = value;
        cameraFollow.enabled = value;
    }
}
