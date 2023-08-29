using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4;
    public float invincibleTime = 1f;
    public HealthBar healthbar;
    [HideInInspector] public bool invincible = false;
    public int health;
    public AudioSource hurt;
    public AudioSource death;
    public GameObject gameOver;
    public GameObject hud;

    bool blink = false;
    float blinkTimer = 0.05f;
    public SpriteRenderer sprite;
    PlayerMovement player;

    private void Start()
    {
        health = maxHealth;
        healthbar.slider.maxValue = maxHealth;
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (blink)
        {
            if (blinkTimer <= 0)
            {
                Blink();
                blinkTimer = 0.05f;
            }
            else
            {
                blinkTimer -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (player.state != PlayerMovement.States.dead)
        {
            health -= dmg;
            StartCoroutine(ActivateInvincibility(invincibleTime));
            StartCoroutine(StartBlink(invincibleTime));

            healthbar.slider.value = health;

            hurt.Play();

            if (health <= 0)
            {
                death.Play();
                gameOver.SetActive(true);
                hud.SetActive(false);
                player.state = PlayerMovement.States.dead;
            }
        }
    }

    private IEnumerator ActivateInvincibility(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    private IEnumerator StartBlink(float time)
    {
        blink = true;
        yield return new WaitForSeconds(time);
        blink = false;
        sprite.enabled = true;
    }

    private void Blink()
    {
        if (sprite.enabled)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }
    }
}
