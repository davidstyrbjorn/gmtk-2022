using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Vector3 heartCardOrigin = new Vector3(0, 0, 0);
    public Sprite[] healthSprites = new Sprite[10];

    List<GameObject> healthCardObjects = new List<GameObject>();
    int currentHP = 0;
    // Start is called before the first frame update
    void Start()
    {
		for (int i = 0; i < healthSprites.Length; i++)
		{
            Canvas canvas = gameObject.GetComponent<Canvas>();

            GameObject imageObject = new GameObject("HealthCard_" + i);

            RectTransform trans = imageObject.AddComponent<RectTransform>();
            trans.transform.SetParent(canvas.transform); // setting parent
            trans.localScale = Vector3.one;
            trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
            trans.sizeDelta = new Vector2(150, 200); // custom size

            Image image = imageObject.AddComponent<Image>();
            image.sprite = healthSprites[i];
            imageObject.transform.SetParent(canvas.transform);

            imageObject.SetActive(false);

            healthCardObjects.Add(imageObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int hp = 0;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		for (int i = 0; i < players.Length; i++)
		{
            GameObject go = players[i];
            if (go != null)
            {
                Health health = go.GetComponent<Health>();
                if (health != null)
                {
                    hp = health.hp;
                }
            }
        }

        if (currentHP != hp)
        {
            currentHP = hp;
            int showedCards = 3;
            for (int i = 0; i < healthCardObjects.Count; i++)
            {
                GameObject healthCard = healthCardObjects[i];
                if (i > currentHP || i < currentHP - showedCards)
				{
                    healthCard.SetActive(false);
				}
                else
				{
                    healthCard.SetActive(true);
                    RectTransform rectTransform = healthCard.GetComponent<RectTransform>();
                    int slotIndex = (currentHP - i);
                    int falloff = 7 * slotIndex * slotIndex;
                    int offset = slotIndex * 50;

                    Vector3 anchPosition = new Vector3(heartCardOrigin.x, heartCardOrigin.y, heartCardOrigin.z);
                    anchPosition.x = -offset + falloff;
                    if (i == currentHP)
                        anchPosition.y -= 30;

                    rectTransform.anchoredPosition = anchPosition;
                }
			}
        }
    }
}
