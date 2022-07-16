using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockScore : MonoBehaviour
{
    public TextMeshProUGUI text;
    private GamblingManager gamblingManager;
    private ScoreFunction scoreFunc;
    private DiceManager diceManager;

    private void Start()
    {
        scoreFunc = GetComponent<ScoreFunction>();
        gamblingManager = FindObjectOfType<GamblingManager>();
        diceManager = FindObjectOfType<DiceManager>();
    }

    public void OnMouseEnter()
    {
        if (!scoreFunc.isLocked)
            text.color = new Color(0, 1, 0);
    }
    public void OnMouseExit()
    {
        if (!scoreFunc.isLocked)
            text.color = new Color(1, 1, 1);
    }
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!scoreFunc.isLocked)
            {
                scoreFunc.isLocked = true;
                text.color = new Color(1, 0, 0);
                gamblingManager.resetThrows();
                diceManager.UnlockAllDice();

                FindObjectOfType<GameManager>().OnGamblingOver();
                gamblingManager.ToggleGambling(false);
            }
        }
    }
}
