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
    public GameObject scoreCategories;
    public GameObject canvasScoreboard;

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
        foreach (ScoreFunction score in scoreCategories.GetComponentsInChildren<ScoreFunction>())
        {
            if (!score.isLocked)
                score.CountScore(results);
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
        canvasScoreboard.gameObject.SetActive(value);

        // Throw dice if we start the gambling
        if (value)
        {
            rollDice();
        }
    }
}
