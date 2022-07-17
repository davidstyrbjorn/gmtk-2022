using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Score: " + GamblingManager.getScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
