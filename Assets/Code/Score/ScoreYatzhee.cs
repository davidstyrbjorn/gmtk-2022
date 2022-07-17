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

        string s = $@"<align=left>Yahtzee<line-height=0>
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
