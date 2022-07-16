using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFullHouse : ScoreFunction
{
    public override int CountScore(int[] results)
    {
        score = 0;
        int triplet = 0;
        int pair = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] == 2) pair = i + 1;
            else if (results[i] == 3) triplet = i + 1;
        }

        if (triplet != 0 && pair != 0) score = 30;

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
