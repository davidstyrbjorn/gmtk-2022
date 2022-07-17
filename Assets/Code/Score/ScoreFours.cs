using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFours : ScoreFunction
{
    const float FOURS_MAX = 24;

    public override int CountScore(int[] results)
    {
        score = 0;
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] >= 4)
            {
                score = (i + 1) * 4;
                break;
            }
        }

        string s = $@"<align=left>Four of a kind<line-height=0>
        <align=right>{score.ToString()}<line-height=1em>";
        GetComponent<TMPro.TextMeshProUGUI>().SetText(s);

        return score;
    }

    public override float GetMultiplierRatio()
    {
        return 1 + (score / FOURS_MAX);
    }

    public override string GetTooltipText()
    {
        if (score == 0)
        {
            return "Omit line. No Four of a kind";
        }
        var gun = "Nothing";
        var completions = FindObjectOfType<GamblingManager>().completions;
        if (completions == 0) gun = "Revolver";
        if (completions == 1) gun = "Shotgun";
        if (completions == 2) gun = "Tommygun";
        return $"Gain <color=green>+{Mathf.RoundToInt((GetMultiplierRatio() - 1) * 100.0f)}%</color> fire rate increase ({gun})";
    }

    void Start()
    {
        isLocked = false;
        score = 0;
    }
}
