using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreStraight : ScoreFunction
{
    public override int CountScore(int[] results)
    {
        score = 0;
        if (results.SequenceEqual(new int[6] { 0, 1, 1, 1, 1, 1 }) || results.SequenceEqual(new int[6] { 1, 1, 1, 1, 1, 0 })) score = 40;

        string s = $@"<align=left>Straight<line-height=0>
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
