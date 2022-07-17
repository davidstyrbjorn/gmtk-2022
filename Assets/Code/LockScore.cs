using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LockScore : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;

    private ScoreFunction scoreFunc;
    private DiceManager diceManager;

    private void Start()
    {
        scoreFunc = GetComponent<ScoreFunction>();
        text = GetComponent<TextMeshProUGUI>();
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

    public void OnShowTooltip()
    {
        // Set correct tooltip text
        FindObjectOfType<GamblingManager>().tooltip.GetComponentInChildren<TextMeshProUGUI>()
            .SetText(GetComponent<ScoreFunction>().GetTooltipText());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!scoreFunc.isLocked)
        {
            GamblingManager gamblingManager = FindObjectOfType<GamblingManager>();
            scoreFunc.isLocked = true;
            text.color = new Color(1, 0, 0);
            gamblingManager.addTotalScore(scoreFunc.score);
            gamblingManager.resetThrows();
            FindObjectOfType<DiceManager>().UnlockAllDice();

            gamblingManager.OnLock();

            //Is the sheet completed?
            if (gamblingManager.AttemptResetSheet())
            {
                gamblingManager.ResetColors();
            }

            FindObjectOfType<GameManager>().OnGamblingOver();
            gamblingManager.ToggleGambling(false);
        }
    }

    public void ResetColor()
    {
        text.color = new Color(0, 0, 0);
    }

    // Some hover styles
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Underline;
        // showtooltip
        FindObjectOfType<GamblingManager>().tooltip.SetActive(true);
        OnShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Normal;
        FindObjectOfType<GamblingManager>().tooltip.SetActive(false);

    }
}
