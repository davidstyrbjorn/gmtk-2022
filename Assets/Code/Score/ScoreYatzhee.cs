using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreYatzhee : ScoreFunction
{
    public override int CountScore(int[] results)
    {
        score = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] == 5)
            {
                score = 50;
                break;
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
