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

    public override float GetMultiplierRatio()
    {
        return 1.0f;
    }

    public override string GetTooltipText()
    {
        if (score == 0)
        {
            return "Omit line. No Straight";
        }
        return "Refills your <color=red>vigor</color>";
    }

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        score = 0;
    }
}
