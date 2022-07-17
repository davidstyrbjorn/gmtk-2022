using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int score = GamblingManager.getScore();
        GetComponent<TextMeshProUGUI>().text = "Yatzhee Score: " + score.ToString();
    }
}
