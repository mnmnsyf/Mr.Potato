using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;            //�����ƶ����ٶ�
    public int HP = 1;
    public Sprite deadEnemy;
    public Sprite damagedEnemy;
    public AudioClip[] deathClips;      
    public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.            
    public float deathSpinMin = -100f;
    public float deathSpinMax = 100f;

    private Rigidbody2D m_Rigidbody;           //�������ù���������������
    private SpriteRenderer ren;             //Reference to the sprite renderer.
    private Transform frontCheck;
    private bool dead = false;
    //private Score score;                //���õ÷ֽű�
    private LayerMask m_LayerMask;

    private void Awake()
    {
        // ��������
        m_Rigidbody = GetComponent<Rigidbody2D>();
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
<<<<<<< HEAD
        //score = GameObject.Find("Score").GetComponent<Score>();
=======
>>>>>>> parent of bc44874 (道具（弹药箱、医疗包）生成、拾取、使用（安放炸弹）及相关音特效)
    }

    private void Start()
    {
        m_LayerMask = LayerMask.GetMask("Obstacle");//��ʾֱ�ӻ��Obstacle���Layer��Ӧ��LayerMask
        Destroy(gameObject, 8);
    }

    private void FixedUpdate()
    {
        // ת��
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, m_LayerMask);//�˺������ڼ��2D������ĳ��λ�ã�Vector2 point�������е���ײ��
        if (frontHits.Length > 0)
        {
            Flip();
        }
        // foreach (Collider2D c in frontHits)
        // {
        //   if (c.tag == "Obstacle")
        //  {
        //      Flip();
        //       break;// ת�� & ֹͣ���������ײ��
        //   }
        // }

        // ����x�����ٶ�
        m_Rigidbody.velocity = new Vector2(transform.localScale.x * moveSpeed, m_Rigidbody.velocity.y);

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
<<<<<<< HEAD

        //Increase the score by 100 points
        //score.score += 100;

       dead = true;

        // ���������ת�����һ��Ť��
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));
=======
        dead = true;
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));// ������ת�����һ��Ť��
>>>>>>> parent of bc44874 (道具（弹药箱、医疗包）生成、拾取、使用（安放炸弹）及相关音特效)

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
 
    // Update is called once per frame
    void Update()
    {
        
    }
}
