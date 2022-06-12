using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;             // The player's health.
    public float repeatDamagePeriod = 2f;   // How frequently the player can be damaged.
    public float hurtForce = 10f;           // The force with which the player is pushed when hurt.
    public float damageAmount = 10f;        // The amount of damage to take when enemies touch the player
    public AudioClip[] ouchClips;           // Array of clips to play when the player is damaged.

    private SpriteRenderer healthBar;       
    private float lastHitTime;
    private Vector3 healthScale;
    private PlayerCtrl playerControl;
    private Animator anim;
    void Awake()
    {
        playerControl = GetComponent<PlayerCtrl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        healthScale = healthBar.transform.localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // 如果碰撞的游戏物体是一个敌人
        if (col.gameObject.tag == "Enemy")
        {
            // 如果时间超过最后一次命中的时间加上两次命中之间的时间
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                // 如果玩家仍然拥有生命值
                if (health > 0f)
                {
                    // 受到伤害并重置最后的时间
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                }
                // 如果玩家没有生命值 让他掉到河里去重新加载关卡
                else
                {
                    //找到游戏物体上所有的碰撞器，并将它们都设置为触发器triggers.
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols)
                    {
                        c.isTrigger = true;
                    }

                    // /移动玩家的所有sprite部分到前面
                    SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer s in spr)
                    {
                        s.sortingLayerName = "UI";
                    }

                    // 禁用 Player Control脚本
                    GetComponent<PlayerCtrl>().enabled = false;

                    //关闭Gun脚本以阻止一个死人射击一个不存在的火箭炮
                    GetComponentInChildren<Gun>().enabled = false;

                    // 触发'Die'动画状态
                    anim.SetTrigger("Die");
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

        GetComponent<PlayerCtrl>().enabled = false;// ... disable user Player Control script
        playerControl.enabled = false;
        GetComponentInChildren<Gun>().enabled = false;//停止射击
        anim.SetTrigger("Die");

        //销毁血条
        GameObject go = GameObject.Find("UI_HealthBar");
        Destroy(go);
    }
    void TakeDamage (Transform enemy)
    {
        playerControl.bJump = false;

        // 创造一个从敌人到玩家的推力
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

        // 在矢量方向上为玩家添加一个力，并乘以hurtForce
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

        health -= damageAmount;

        UpdateHealthBar();

        int i = Random.Range(0, ouchClips.Length);
        AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
    }

    public void UpdateHealthBar()
    {
        // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

        // Set the scale of the health bar to be proportional to the player's health.
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

}
