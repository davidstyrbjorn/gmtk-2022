using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        string s = $@"<align=left>Full House<line-height=0>
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
        var gun = "Nothing";
        var completions = FindObjectOfType<GamblingManager>().completions;
        if (completions == 0) gun = "Revolver";
        if (completions == 1) gun = "Shotgun";
        if (completions == 2) gun = "Tommygun";
        return $"Gives your {gun} piercing rounds";
    }

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        score = 0;
    }


}
