using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTwoPair : ScoreFunction
{
    public override int CountScore(int[] results)
    {
        score = 0;
        int pairOne = 0;
        int pairTwo = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] >= 2)
            {
                if (pairOne == 0) pairOne = i + 1;
                else if (pairTwo == 0) pairTwo = i + 1;
            }
        }

        if (pairOne != 0 && pairTwo != 0) score = pairOne * 2 + pairTwo * 2;

        string s = $@"<align=left>Two of a kind<line-height=0>
        <align=right>{score.ToString()}<line-height=1em>";
        GetComponent<TMPro.TextMeshProUGUI>().SetText(s);

        return score;
    }

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        score = 0;
    }
}
