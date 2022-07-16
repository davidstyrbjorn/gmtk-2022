using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreThrees : ScoreFunction 
{
    public override int CountScore(int[] results)
    {
        score = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if(results[i] >= 3)
            {
                score = (i + 1) * 3;
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
