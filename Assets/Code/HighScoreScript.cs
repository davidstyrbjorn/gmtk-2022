using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int highScore = GamblingManager.getHighScore();
        int score = GamblingManager.getScore();
        if (score > highScore)
        {
            GamblingManager.setHighScore(score);
            highScore = score;
        }
        GetComponent<TextMeshProUGUI>().text = "High Score: " + highScore.ToString();
    }
}
