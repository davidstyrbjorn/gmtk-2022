using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GamblingManager : MonoBehaviour
{
    public GameObject tooltip;

    [SerializeField]
    public bool needsUpdate = false;
    public DiceManager diceManager;

    private int[] results = new int[6];
    [SerializeField]
    private int throws = 0;

    [SerializeField] private GameObject noThrowsAlert;

    public int completions = 0;
    private static int totalScore = 0;
    private static int highScore = 0;
    public GameObject scoreCategories;
    public GameObject canvasScoreboard;
    private GunBehavior gunBehavior;

    const int ONE_PAIR_MAX = 12;

    private Dictionary<string, bool> categoryFlag = new Dictionary<string, bool>();

    void Awake()
    {
        gunBehavior = FindObjectOfType<GunBehavior>();
        noThrowsAlert.SetActive(false);
    }

    public static int getScore() { return totalScore; }
    public static int getHighScore() { return highScore; }
    public static void resetScore() { totalScore = 0; }
    public static void setHighScore(int newHighScore) { highScore = newHighScore; }
    public void addTotalScore(int toAdd) { totalScore += toAdd; }

    public void resetThrows()
    {
        throws = 0;
    }

    IEnumerator AlertTimer()
    {
        yield return new WaitForSeconds(1.0f);
        noThrowsAlert.SetActive(false);
    }

    public void rollDice()
    {
        if (!diceManager.AllDiceSettled()) return;
        if (throws >= 3)
        {
            noThrowsAlert.SetActive(true);
            StartCoroutine(AlertTimer());
            return;
        }
        needsUpdate = true;
        diceManager.Throw();
        throws++;
        // Play SFX & Shake the camera
        FindObjectOfType<SfxManager>().PlaySound("dice_throw", 1.0f, 1.0f);
        FindObjectOfType<CameraShake>().DoShake(0.12f, 0.25f);
    }

    public void updateAll(int[] diceResults) // Update results and all aspects of UI.
    {
        updateResults(diceResults);
        updateScores();
        needsUpdate = false;
    }

    public void updateResults(int[] diceResults) // Update dice results.
    {
        results = new int[6]; //Reset the results
        for (int i = 0; i < diceResults.Length; i++)
        {
            results[diceResults[i] - 1]++;
        }
    }

    public void updateScores()
    {
        int totalScore = 0;
        foreach (ScoreFunction score in scoreCategories.GetComponentsInChildren<ScoreFunction>())
        {
            if (!score.isLocked)
                totalScore += score.CountScore(results);
        }

        // We do this flag type of deal to increase the amount of sunlight my body gets before
        // i burn up and this code melts into nothing but atoms 

        // Threshold for playing gambling success/fail sfx?
        if (totalScore >= 15)
        {
            FindObjectOfType<SfxManager>().PlaySound("gamble_success", 0.5f);
        }
        if (totalScore == 0)
        {
            FindObjectOfType<SfxManager>().PlaySound("gamble_fail", 0.5f);
        }
    }

    public void OnLock()
    {

        if (!categoryFlag.GetValueOrDefault("four_of_a_kind"))
        {
            if (FindObjectOfType<ScoreFours>().score != 0 && FindObjectOfType<ScoreFours>().isLocked)
            {
                categoryFlag.Add("four_of_a_kind", true);
                // Four of a kind firerate
                var ratio = FindObjectOfType<ScoreFours>().GetMultiplierRatio();
                print("GO FOUR OF A KIND! " + ratio);
                switch (completions)
                {
                    case 0:
                        gunBehavior.pistolData.fireRate *= ratio;
                        break;
                    case 1:
                        gunBehavior.shotgunData.fireRate *= ratio;
                        break;
                    case 2:
                        gunBehavior.tommygunData.fireRate *= ratio;
                        break;
                }
            }
        }

        // Two pairs decrease spread
        if (!categoryFlag.GetValueOrDefault("two_pairs"))
        {
            if (FindObjectOfType<ScoreTwoPair>().score != 0 && FindObjectOfType<ScoreTwoPair>().isLocked)
            {

                categoryFlag.Add("two_pairs", true);
                // At most we half the spread
                var ratio = FindObjectOfType<ScoreTwoPair>().GetMultiplierRatio();
                print("GO TWO PAIRS! " + ratio);
                switch (completions)
                {
                    case 0:
                        gunBehavior.pistolData.spread /= ratio;
                        break;
                    case 1:
                        gunBehavior.shotgunData.spread /= ratio;
                        break;
                    case 2:
                        gunBehavior.tommygunData.spread /= ratio;
                        break;
                }
            }
        }

        if (!categoryFlag.GetValueOrDefault("pair"))
        {
            if (FindObjectOfType<ScoreOnePair>().score != 0 && FindObjectOfType<ScoreOnePair>().isLocked)
            {
                categoryFlag.Add("pair", true);
                if (FindObjectOfType<PlayerController>().TryGetComponent<Health>(out Health hp))
                {
                    hp.hp++;
                    hp.hp = Mathf.Min(hp.hp, 10);
                }
            }
        }

        // Straight heals to max
        if (!categoryFlag.GetValueOrDefault("straight"))
        {
            if (FindObjectOfType<ScoreStraight>().score != 0 && FindObjectOfType<ScoreStraight>().isLocked)
            {
                print("GO STRAIGHT!");
                categoryFlag.Add("straight", true);
                FindObjectOfType<PlayerController>().GetComponent<Health>().hp = 10;
            }
        }

        // Three of a kind increases movement speed
        if (!categoryFlag.GetValueOrDefault("three_of_a_kind"))
        {
            if (FindObjectOfType<ScoreThrees>().score != 0 && FindObjectOfType<ScoreThrees>().isLocked)
            {
                categoryFlag.Add("three_of_a_kind", true);
                // Max we increase movement speed by 1.5 factor
                var ratio = FindObjectOfType<ScoreThrees>().GetMultiplierRatio();
                print("GO THREE OF A KIND! " + ratio);
                FindObjectOfType<PlayerMovement>().moveSpeed *= ratio;
            }
        }

        if (!categoryFlag.GetValueOrDefault("full_house"))
        {
            if (FindObjectOfType<ScoreFullHouse>().score != 0 && FindObjectOfType<ScoreFullHouse>().isLocked)
            {
                categoryFlag.Add("full_house", true);
                switch (completions)
                {
                    case 0:
                        gunBehavior.pistolData.piercing++;
                        break;
                    case 1:
                        gunBehavior.shotgunData.piercing++;
                        break;
                    case 2:
                        gunBehavior.tommygunData.piercing++;
                        break;
                }
            }
        }

        // Yahtzee kills all enemies
        if (!categoryFlag.GetValueOrDefault("yahtzee"))
        {
            if (FindObjectOfType<ScoreYatzhee>().score != 0 && FindObjectOfType<ScoreYatzhee>().isLocked)
            {
                print("GO YAHTZEE!");
                categoryFlag.Add("yahtzee", true);
                // Remove all enemies
                foreach (var enemy in FindObjectsOfType<Enemy>())
                {
                    Destroy(enemy.gameObject);
                }
            }
        }
    }

    public void rollYatzhee()
    {
        updateAll(new int[6] { 0, 0, 0, 0, 0, 5 });
    }

    public void rollStraight()
    {
        updateAll(new int[5] { 1, 1, 1, 1, 1 });
    }

    public void ToggleGambling(bool value)
    {
        diceManager.gameObject.SetActive(value);
        // canvasScoreboard.gameObject.SetActive(value);
        if (value)
        {
            canvasScoreboard.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            canvasScoreboard.transform.localScale = new Vector3(0, 0, 0);
        }

        // Throw dice if we start the gambling
        if (value)
        {
            rollDice();
        }
    }

    public bool AttemptResetSheet()
    {
        var scores = scoreCategories.GetComponentsInChildren<ScoreFunction>();
        foreach (ScoreFunction score in scores)
        {
            if (!score.isLocked) return false;
        }

        foreach (ScoreFunction score in scores)
        {
            score.isLocked = false;
            score.score = 0;
        }
        completions++;

        // Reset the flags
        categoryFlag.Clear();

        return true;
    }

    public void ResetColors()
    {
        foreach (LockScore scoreLock in scoreCategories.GetComponentsInChildren<LockScore>())
        {
            scoreLock.ResetColor();
        }
    }
}
