using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnePair : ScoreFunction
{
    public override int CountScore(int[] results)
    {
        score = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] >= 2)
            {
                score = (i + 1) * 2;
            }
        }

        scoreVisualizer.text = score.ToString();

        return score;
    }

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        score = 0;
    }
}
