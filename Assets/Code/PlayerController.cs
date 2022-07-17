using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : MonoBehaviour
{
    private Health health;
    private int lastFrameHealth = 0;

    private SfxManager sfxManager;

    public GameManager.SECTION currentSection;
    private Animator animator;

    public PostProcessVolume pp;

    [System.NonSerialized]
    public bool deathFlag = false;

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
        if (deathFlag) return;

        if (lastFrameHealth != health.hp)
        {
            // We have taken damage
            sfxManager.PlaySound("player_hurt", 0.65f);
            Camera.main.GetComponent<CameraShake>().DoShake(0.1f, 0.6f);
        }

        if (health.hp <= 0)
        {
            deathFlag = true;
            // Game over!
            animator.SetTrigger("died");
            FindObjectOfType<GameManager>().ToggleBarFight(false);
            // Fade out all enemies, disable ALOT of stuff
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.GetComponent<SpriteRenderer>().color = Color.clear;
            }
            foreach (var lm in FindObjectsOfType<LookAtMouse>())
            {
                lm.gameObject.SetActive(false);
            }
            FindObjectOfType<CameraFollow>().enabled = true;
            FindObjectOfType<Canvas>().enabled = false;
            StartCoroutine(DeathRoutine());
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

    public IEnumerator DeathRoutine()
    {
        // Fade effect
        const float FADE_SPEED = 0.1f;
        if (pp.profile.TryGetSettings<Vignette>(out Vignette vig))
        {
            while (vig.intensity <= 1)
            {
                vig.intensity.Interp(vig.intensity, 2.5f, FADE_SPEED * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("GameOverScene");
    }
}
