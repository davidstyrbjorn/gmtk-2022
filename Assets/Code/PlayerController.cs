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

    private float damageTime = -100;

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

        if (lastFrameHealth > health.hp)
        {
            // We have taken damage
            sfxManager.PlaySound("player_hurt", 0.65f);
            Camera.main.GetComponent<CameraShake>().DoShake(0.1f, 0.6f);

            damageTime = Time.time;
        }

        float damageEffectSpeed = 2f;
        float epsilon = 0.5f;
        float t = Time.time - damageTime;
        if (t < (1 / damageEffectSpeed) + epsilon)
        {
            List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();
            spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
            spriteRenderers.Add(GetComponent<SpriteRenderer>());

            for (int i = 0; i < spriteRenderers.Count; i++)
            {
                SpriteRenderer spriteRenderer = spriteRenderers[i];
                float gAndB = Mathf.Lerp(0, 1, t);
                float a = Mathf.Lerp(0.3f, 1, t);


                Color newColor = new Color(1f, gAndB * damageEffectSpeed, gAndB * damageEffectSpeed, a * damageEffectSpeed);
                spriteRenderer.color = newColor;
            }
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

    void OnTriggerStay2D(Collider2D other)
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
