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
    private Score score;				//引用得分脚本


    private void Awake()
    {
        // 设置引用
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
    }

    private void FixedUpdate()
    {
        // 转身
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);
        foreach (Collider2D c in frontHits)
        {
            if (c.tag == "Obstacle")
            {
                Flip();
                break;// 转身 & 停止检测其他碰撞体
            }
        }

        // 设置x方向速度
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // 如果敌人还有一个生命值而且有一个受伤的sprite
        if (HP == 1 && damagedEnemy != null)
            ren.sprite = damagedEnemy;

        // 没血且death状态未修改为true
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
        // 找出所有的组成部分
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in otherRenderers)// Disable all of them sprite renderers.
        {
            s.enabled = false;
        }

        ren.enabled = true;
        ren.sprite = deadEnemy;
        dead = true;
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));// 允许旋转并添加一个扭矩

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        //随机播放死亡音效
        int i = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

        // 得分 Create a vector that is just above the enemy.
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
