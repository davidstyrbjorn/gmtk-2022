using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GamblingManager : MonoBehaviour
{
    [SerializeField]
    public bool needsUpdate = false;
    public DiceManager diceManager;

    private int[] results = new int[6];
    [SerializeField]
    private int throws = 0;

    public int completions = 0;
    public GameObject scoreCategories;
    public GameObject canvasScoreboard;

    private Dictionary<string, System.Func<int[], int>> categoryToFunc = new Dictionary<string, System.Func<int[], int>>();

    void Awake()
    {
        // Add all the categorical functions
        // categoryToFunc.Add("One Pair", (int[] x) =>
        // {
        //     return 1;
        // });
        // categoryToFunc.Add("One Pair", (int[] x) =>
        // {
        //     return 1;
        // });
        // categoryToFunc.Add("One Pair", (int[] x) =>
        // {
        //     return 1;
        // });
        // categoryToFunc.Add("One Pair", (int[] x) =>
        // {
        //     return 1;
        // });
        // categoryToFunc.Add("One Pair", (int[] x) =>
        // {
        //     return 1;
        // });
        // categoryToFunc.Add("One Pair", (int[] x) =>
        // {
        //     return 1;
        // });

        // if (categoryToFunc.TryGetValue("One pair", out var scoreFunc))
        // {
        //     int[] r = new int[6];
        //     scoreFunc.Invoke(r);
        // }
    }

    public void resetThrows()
    {
        throws = 0;
    }

    public void rollDice()
    {
        if (throws >= 3) return;
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

        // Did i get a pair of two's?

        // Threshold for playing gambling success/fail sfx?
        if (totalScore >= 15)
        {
            FindObjectOfType<SfxManager>().PlaySound("gamble_success");
        }
        if (totalScore == 0)
        {
            FindObjectOfType<SfxManager>().PlaySound("gamble_fail");
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
        }
        completions++;
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
