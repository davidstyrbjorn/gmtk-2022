using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSceneController : MonoBehaviour
{
    public List<GameObject> frames = new List<GameObject>();
    public GameObject blackoutScreen;

    float fadeValue;
    bool increaseFade = false;
    float fadeSpeed = 3f;

    int currentFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        Image blackoutImage = blackoutScreen.GetComponent<Image>();
        fadeValue = blackoutImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentFrame = (currentFrame + 1);
            if (currentFrame >= frames.Count)
                return;

            increaseFade = true;
        }

        Image blackoutImage = blackoutScreen.GetComponent<Image>();

        if (increaseFade)
            fadeValue += fadeSpeed * Time.deltaTime;
        else
            fadeValue -= fadeSpeed * Time.deltaTime;

        fadeValue = Mathf.Clamp01(fadeValue);
        Color newColor = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, fadeValue);

        blackoutImage.color = newColor;

        if (increaseFade && fadeValue >= 1)
        {
            int previousFrame = currentFrame == 0 ? frames.Count - 1 : currentFrame - 1;
            increaseFade = false;
            frames[previousFrame].SetActive(false);
            frames[currentFrame].SetActive(true);
        }
    }
}
