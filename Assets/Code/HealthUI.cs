using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Vector2 heartCardOrigin = new Vector2(100, -25);
    public Sprite[] healthSprites = new Sprite[10];
    private List<Image> cards = new List<Image>();

    List<GameObject> healthCardObjects = new List<GameObject>();
    private GameManager gameManager;
    int currentHP = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        for (int i = 0; i < healthSprites.Length; i++)
        {
            Canvas canvas = gameObject.GetComponent<Canvas>();


            GameObject imageObject = new GameObject("HealthCard_" + i);

            RectTransform trans = imageObject.AddComponent<RectTransform>();
            trans.transform.SetParent(canvas.transform);
            trans.localScale = Vector3.one;
            trans.anchoredPosition = heartCardOrigin;
            trans.sizeDelta = new Vector2(75, 100);

            trans.anchorMin = new Vector2(0, 1);
            trans.anchorMax = new Vector2(0, 1);

            Image image = imageObject.AddComponent<Image>();
            cards.Add(image);
            image.sprite = healthSprites[i];
            imageObject.transform.SetParent(canvas.transform);

            imageObject.SetActive(false);
            healthCardObjects.Add(imageObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GAME_STATE.GAMBLING)
        {
            foreach (var card in cards)
            {
                card.color = Vector4.MoveTowards(card.color, new Color(0, 0, 0, 0.2f), 2.0f * Time.deltaTime);
            }
        }
        else
        {
            foreach (var card in cards)
            {
                card.color = Vector4.MoveTowards(card.color, Color.white, 2.0f * Time.deltaTime);
            }
        }

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
                int hpIndex = currentHP - 1;
                GameObject healthCard = healthCardObjects[i];
                if (i > hpIndex || i < hpIndex - showedCards)
                {
                    healthCard.SetActive(false);
                }
                else
                {
                    healthCard.SetActive(true);
                    RectTransform rectTransform = healthCard.GetComponent<RectTransform>();
                    int slotIndex = (hpIndex - i);
                    int falloff = 5 * slotIndex * slotIndex;
                    int offset = slotIndex * 20;

                    Vector2 anchPosition = new Vector2(heartCardOrigin.x, heartCardOrigin.y);
                    anchPosition.x += -offset + falloff;
                    if (i == hpIndex)
                        anchPosition.y -= 15;

                    rectTransform.anchoredPosition = anchPosition;
                }
            }
        }
    }
}
