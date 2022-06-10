using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;            //怪物移动的速度
    public int HP = 2;
    public Sprite deadEnemy;
    public Sprite damagedEnemy;
    public AudioClip[] deathClips;      
    public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.            
    public float deathSpinMin = -100f;
    public float deathSpinMax = 100f;

    private Rigidbody2D m_Rigidbody;           //用于设置怪物对象的物理属性
    private SpriteRenderer ren;             //Reference to the sprite renderer.
    private Transform frontCheck;
    private bool dead = false;
    private Score score;                //引用得分脚本
    private LayerMask m_LayerMask;

    private void Awake()
    {
        // 设置引用
        m_Rigidbody = GetComponent<Rigidbody2D>();
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
    }

    private void Start()
    {
        m_LayerMask = LayerMask.GetMask("Obstacle");//表示直接获得Obstacle这个Layer对应的LayerMask
    }

    private void FixedUpdate()
    {
        // 转身
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, m_LayerMask);//此函数用于检测2D场景中某个位置（Vector2 point）处所有的碰撞体
        if (frontHits.Length > 0)
        {
            Flip();
        }
        // foreach (Collider2D c in frontHits)
        // {
        //   if (c.tag == "Obstacle")
        //  {
        //      Flip();
        //       break;// 转身 & 停止检测其他碰撞体
        //   }
        // }

        // 设置x方向速度
        m_Rigidbody.velocity = new Vector2(transform.localScale.x * moveSpeed, m_Rigidbody.velocity.y);

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
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
