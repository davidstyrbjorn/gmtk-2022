using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Health health;
    private int lastFrameHealth = 0;

    private SfxManager sfxManager;

    public GameManager.SECTION currentSection;
    private Animator animator;

    void Start()
    {
        currentSection = GameManager.SECTION.BAR;
        health = GetComponent<Health>();
        lastFrameHealth = health.hp;
        sfxManager = FindObjectOfType<SfxManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (lastFrameHealth != health.hp)
        {
            // We have taken damage
            sfxManager.PlaySound("player_hurt", 0.65f);
            Camera.main.GetComponent<CameraShake>().DoShake(0.1f, 0.6f);
        }

        if (health.hp <= 0)
        {
            // Game over!
            // SceneManager.LoadScene("GameOverScene");
            animator.SetTrigger("died");
            FindObjectOfType<GameManager>().ToggleBarFight(false);
            // Fade out all enemies
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            foreach (var lm in FindObjectsOfType<LookAtMouse>())
            {
                lm.enabled = false;
            }
        }

        lastFrameHealth = health.hp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bar")
        {
            currentSection = GameManager.SECTION.BAR;
        }
        else if (other.tag == "kitchen")
        {
            currentSection = GameManager.SECTION.KITCHEN;
        }
        else if (other.tag == "game")
        {
            currentSection = GameManager.SECTION.GAME;
        }
    }
}
