using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer healthBar;       // Reference to the sprite renderer of the health bar.
    public float health = 100f;             // The player's health.
    public float repeatDamagePeriod = 2f;   // How frequently the player can be damaged.
    public float hurtForce = 10f;           // The force with which the player is pushed when hurt.
    public float damageAmount = 10f;        // The amount of damage to take when enemies touch the player
    public AudioClip[] ouchClips;           // Array of clips to play when the player is damaged.

    private float lastHitTime;
    private Vector3 healthScale;
    private PlayerCtrl playerControl;
    private Animator anim;
    void Awake()
    {
        playerControl = GetComponent<PlayerCtrl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // Getting the intial scale of the healthbar (whilst the player has full health).
        healthScale = healthBar.transform.localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            // ... and if the time exceeds the time of the last hit plus the time between hits...
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                //可以再次减血
                if (health > 0f)
                {
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                }

            }
        }
    }
    void death()
    {
        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        // Move all sprite parts of the player to the front
        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }

        //GetComponent<PlayerCtrl>().enabled = false;
        playerControl.enabled = false;// ... disable user Player Control script
        GetComponentInChildren<Gun>().enabled = false;// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
        //anim.SetTrigger("Die");

        //销毁血条
        GameObject go = GameObject.Find("UI_HealthBar");
        Destroy(go);
    }
    void TakeDamage(Transform enemy)
    {
        // Make sure the player can't jump.
        playerControl.bJump = false;

        // 创造推力 Create a vector that's from the enemy to the player with an upwards boost.
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

        // 施加推力 Add a force to the player in the direction of the vector and multiply by the hurtForce.
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
        health -= damageAmount;
        if (health <= 0)
        {
            death();
            // return;
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

        // Set the scale of the health bar to be proportional to the player's health.
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

}
