using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreStraight : ScoreFunction
{
    public override int CountScore(int[] results)
    {
        score = 0;
        if (results.SequenceEqual(new int[6] { 0, 1, 1, 1, 1, 1 })|| results.SequenceEqual(new int[6] { 1, 1, 1, 1, 1, 0 })) score = 40;

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
