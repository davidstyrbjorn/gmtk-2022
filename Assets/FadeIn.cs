using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Image image;
    // Update is called once per frame

    private void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        if(image.color.a <= 0)
        {
            Destroy(gameObject);
        } else
        {
            image.color = Vector4.MoveTowards(image.color, Color.clear, 2 * Time.deltaTime);
        }
    }
}
