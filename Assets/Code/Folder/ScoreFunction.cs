using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ScoreFunction : MonoBehaviour
{
    public TextMeshProUGUI scoreVisualizer;
    protected bool isLocked;
    protected int score;


    public abstract int CountScore(int[] results);
}