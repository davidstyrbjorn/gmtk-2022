using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSelectUI : MonoBehaviour
{
    public RawImage pistolCard;
    public RawImage shotgunCard;
    public RawImage tommygunCard;

    private RawImage[] cards;
    private float selectedY;

    private GamblingManager gamblingManager;
    private GameManager gameManager;
    private GunBehavior gunBehavior;
    private float cardHeight;

    void Start()
    {
        gamblingManager = FindObjectOfType<GamblingManager>();
        gunBehavior = FindObjectOfType<GunBehavior>();
        gameManager = FindObjectOfType<GameManager>();

        cards = new RawImage[] { pistolCard, shotgunCard, tommygunCard };
        selectedY = pistolCard.rectTransform.position.y;
        cardHeight = pistolCard.rectTransform.rect.height;
    }

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
        if (gamblingManager.completions >= 1) shotgunCard.enabled = true;
        if (gamblingManager.completions >= 2) tommygunCard.enabled = true;

        foreach (var card in cards)
        {
            card.rectTransform.position = new Vector3(card.rectTransform.position.x, selectedY - cardHeight / 2, 0);
        }
        cards[gunBehavior.currGunIndex].rectTransform.position = new Vector3(cards[gunBehavior.currGunIndex].rectTransform.position.x, selectedY, 0);
    }
}
