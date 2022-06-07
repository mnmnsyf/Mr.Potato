using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int HP = 2;
    public Sprite deadEnemy;
    public Sprite damagedEnemy;
    public AudioClip[] deathClips;      
    public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.            
    public float deathSpinMin = -100f;
    public float deathSpinMax = 100f;

    private SpriteRenderer ren;             //Reference to the sprite renderer.
    private Transform frontCheck;
    private bool dead = false;
    private Score score;				//���õ÷ֽű�


    private void Awake()
    {
        // ��������
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
    }

    private void FixedUpdate()
    {
        // ת��
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);
        foreach (Collider2D c in frontHits)
        {
            if (c.tag == "Obstacle")
            {
                Flip();
                break;// ת�� & ֹͣ���������ײ��
            }
        }

        // ����x�����ٶ�
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // ������˻���һ������ֵ������һ�����˵�sprite
        if (HP == 1 && damagedEnemy != null)
            ren.sprite = damagedEnemy;

        // ûѪ��death״̬δ�޸�Ϊtrue
        if (HP <= 0 && !dead)
            Death();
    }

    public void Flip()
    {
        Vector3 enemyScale = transform.localScale;
        enemyScale.x *= -1;
        transform.localScale = enemyScale;
    }

    void Death()
    {
        // �ҳ����е���ɲ���
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in otherRenderers)// Disable all of them sprite renderers.
        {
            s.enabled = false;
        }

        ren.enabled = true;
        ren.sprite = deadEnemy;
        dead = true;
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));// ������ת������һ��Ť��

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        //�������������Ч
        int i = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

        // �÷� Create a vector that is just above the enemy.
        Vector3 scorePos;
        scorePos = transform.position;
        scorePos.y += 1.5f;
        Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
    }

    public void Hurt()
    {
        // Reduce the number of hit points by one.
        HP--;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}