using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreThrees : ScoreFunction
{
    const float THREES_MAX = 18;


    public override int CountScore(int[] results)
    {
        score = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] >= 3)
            {
                score = (i + 1) * 3;
                break;
            }
        }

        string s = $@"<align=left>Three of a kind<line-height=0>
        <align=right>{score.ToString()}<line-height=1em>";
        GetComponent<TMPro.TextMeshProUGUI>().SetText(s);

        return score;
    }

    public override float GetMultiplierRatio()
    {
        return 1.0f + ((score / THREES_MAX) * 0.5f);
    }

    public override string GetTooltipText()
    {
        return $"Gain <color=green>+{Mathf.Round((GetMultiplierRatio() - 1.0f) * 100.0f)}%</color> movement speed increase";
    }

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        score = 0;
    }
}
