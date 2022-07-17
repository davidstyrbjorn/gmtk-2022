using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ScoreFunction : MonoBehaviour
{
    public bool isLocked;
    public int score;
    public abstract int CountScore(int[] results);

    public abstract string GetTooltipText();
    public abstract float GetMultiplierRatio();
}
