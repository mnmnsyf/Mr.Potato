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
        // �����ײ����Ϸ������һ������
        if (col.gameObject.tag == "Enemy")
        {
            // ���ʱ�䳬�����һ�����е�ʱ�������������֮���ʱ��
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                // ��������Ȼӵ������ֵ
                if (health > 0f)
                {
                    // �ܵ��˺�����������ʱ��
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                }
                // ������û������ֵ ������������ȥ���¼��عؿ�
                else
                {
                    //�ҵ���Ϸ���������е���ײ�����������Ƕ�����Ϊ������triggers.
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols)
                    {
                        c.isTrigger = true;
                    }

                    // /�ƶ���ҵ�����sprite���ֵ�ǰ��
                    SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer s in spr)
                    {
                        s.sortingLayerName = "UI";
                    }

                    // ���� Player Control�ű�
                    GetComponent<PlayerCtrl>().enabled = false;

                    //�ر�Gun�ű�����ֹһ���������һ�������ڵĻ����
                    GetComponentInChildren<Gun>().enabled = false;

                    // ����'Die'����״̬
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
        GetComponentInChildren<Gun>().enabled = false;//ֹͣ���
        anim.SetTrigger("Die");

        //����Ѫ��
        GameObject go = GameObject.Find("UI_HealthBar");
        Destroy(go);
    }
    void TakeDamage (Transform enemy)
    {
        playerControl.bJump = false;

        // ����һ���ӵ��˵���ҵ�����
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

        // ��ʸ��������Ϊ������һ������������hurtForce
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
