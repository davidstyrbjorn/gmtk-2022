using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreFullHouse : ScoreFunction, IPointerEnterHandler, IPointerExitHandler
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

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
        score = 0;
    }


}
