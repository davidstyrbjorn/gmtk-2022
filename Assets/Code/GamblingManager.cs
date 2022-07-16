using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamblingManager : MonoBehaviour
{
    [SerializeField]
    private int numberOfDice = 5;

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

    public void rollDice()
    {
        results = new int[6]; //Reset the results

        for (int i = 0; i < numberOfDice; i++) //
        {
            int result = Random.Range(1, 7);
            results[result - 1]++;
        }

        updateText();

        checkScore();
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

    public void checkScore()
    {
        foreach(ScoreFunction score in scoreCategories.GetComponentsInChildren<ScoreFunction>())
        {
            score.CountScore(results);
        }
    }

    public void rollYatzhee()
    {
        results = new int[6] { 0, 0, 0, 0, 0, 5 };

        updateText();

        checkScore();
    }

    public void rollStraight()
    {
        results = new int[6] { 0, 1, 1, 1, 1, 1 };

        updateText();

        checkScore();
    }
}
