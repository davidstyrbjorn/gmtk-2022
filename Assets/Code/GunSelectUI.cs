using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectUI : MonoBehaviour
{
    public RawImage pistolCard;
    public RawImage shotgunCard;
    public RawImage tommygunCard;

    public GameObject unlockedWeaponTextObject;

    private RawImage[] cards;
    private float selectedY;

    private GamblingManager gamblingManager;
    private GameManager gameManager;
    private GunBehavior gunBehavior;
    private float cardHeight;

    private Vector2 weaponTextStartPosition;

    void Start()
    {
        gamblingManager = FindObjectOfType<GamblingManager>();
        gunBehavior = FindObjectOfType<GunBehavior>();
        gameManager = FindObjectOfType<GameManager>();

        cards = new RawImage[] { pistolCard, shotgunCard, tommygunCard };
        selectedY = pistolCard.rectTransform.position.y;
        cardHeight = pistolCard.rectTransform.rect.height;

        weaponTextStartPosition = unlockedWeaponTextObject.GetComponent<RectTransform>().localPosition;
    }

    bool wasShotgunUnlocked = false;
    bool wasTommygunUnlocked = false;

    float newWeaponTime = -100;

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

        // Enable disable cards dependent on if they're unlocked or not
        if (gamblingManager.completions >= 1)
        {
            shotgunCard.color = Color.white;
            shotgunCard.enabled = true;

            if (!wasShotgunUnlocked)
            {
                wasShotgunUnlocked = true;
                newWeaponTime = Time.time;
            }
        }
        else
        {
            shotgunCard.color = new Color(0, 0, 0, 0.7f);
            wasShotgunUnlocked = false;
        }

        if (gamblingManager.completions >= 2)
        {
            shotgunCard.color = Color.white;
            tommygunCard.enabled = true;
            if (!wasTommygunUnlocked)
            {
                wasTommygunUnlocked = true;
                newWeaponTime = Time.time;
            }
        }
        else
        {
            tommygunCard.color = new Color(0, 0, 0, 0.7f);
            wasTommygunUnlocked = false;
        }

        float timeSinceNewWeapon = Time.time - newWeaponTime;

        float renderTime = 3f;
        float renderProgression = timeSinceNewWeapon / renderTime;
        if (timeSinceNewWeapon < renderTime)
		{
            TMPro.TextMeshProUGUI textMesh = unlockedWeaponTextObject.GetComponent<TMPro.TextMeshProUGUI>();
            Color newColor = new Color();
            newColor.r = textMesh.color.r;
            newColor.g = textMesh.color.g;
            newColor.b = textMesh.color.b;
            newColor.a = Mathf.Clamp01((renderTime / 2) - renderProgression * 2f);

            textMesh.color = newColor;
            RectTransform rectTransform = unlockedWeaponTextObject.GetComponent<RectTransform>();

            Vector2 anchPosition = new Vector2(weaponTextStartPosition.x, weaponTextStartPosition.y + Mathf.Sin(renderProgression * 5) * 10f);
            rectTransform.anchoredPosition = anchPosition;
        }

        foreach (var card in cards)
        {
            card.rectTransform.position = new Vector3(card.rectTransform.position.x, selectedY - cardHeight / 2, 0);
        }
        cards[gunBehavior.currGunIndex].rectTransform.position = new Vector3(cards[gunBehavior.currGunIndex].rectTransform.position.x, selectedY, 0);
    }
}
