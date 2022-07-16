using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamblingManager : MonoBehaviour
{
    [SerializeField]
    public bool needsUpdate = false;
    [SerializeField]
    private DiceManager diceManager;

    private int[] results = new int[6];
    public TextMeshProUGUI resultText;
    public GameObject scoreCategories;

    public void Start()
    {
        if (!resultText)
        {
            Debug.LogError("No text output for GamblingManager is set!");
        }
    }

    public void rollDice() // TO BE REMOVED
    {
        needsUpdate = true;
        diceManager.Throw();
    }

    public void updateAll(int[] diceResults) // Update results and all aspects of UI.
    {
        updateResults(diceResults);
        updateText();
        updateScores();
        needsUpdate = false;
    }
    
    public void updateResults(int[] diceResults) // Update dice results.
    {
        results = diceResults;
        updateText();
        results = new int[6]; //Reset the results
        for (int i = 0; i < diceResults.Length; i++) 
        {
            results[diceResults[i] - 1]++;
        }
    }

    public void updateText()
    {
        string tempText = "";

        for (int i = 0; i < results.Length; i++)
        {
            tempText += results[i] + " ";
        }

        tempText = tempText[0..^1]; //Remove last space

        resultText.text = tempText;
    }

    public void updateScores()
    {
        foreach(ScoreFunction score in scoreCategories.GetComponentsInChildren<ScoreFunction>())
        {
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
}
