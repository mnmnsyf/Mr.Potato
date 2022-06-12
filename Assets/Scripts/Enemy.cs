using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;            //¹ÖÎïÒÆ¶¯µÄËÙ¶È
    public int HP = 1;
    public Sprite deadEnemy;
    public Sprite damagedEnemy;
    public AudioClip[] deathClips;      
    public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.            
    public float deathSpinMin = -100f;
    public float deathSpinMax = 100f;

    private Rigidbody2D m_Rigidbody;           //ÓÃÓÚÉèÖÃ¹ÖÎï¶ÔÏóµÄÎïÀíÊôĞÔ
    private SpriteRenderer ren;             //Reference to the sprite renderer.
    private Transform frontCheck;
    private bool dead = false;
    //private Score score;                //ÒıÓÃµÃ·Ö½Å±¾
    private LayerMask m_LayerMask;

    private void Awake()
    {
        // ÉèÖÃÒıÓÃ
        m_Rigidbody = GetComponent<Rigidbody2D>();
        ren = transform.Find("body").GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
<<<<<<< HEAD
        //score = GameObject.Find("Score").GetComponent<Score>();
=======
>>>>>>> parent of bc44874 (é“å…·ï¼ˆå¼¹è¯ç®±ã€åŒ»ç–—åŒ…ï¼‰ç”Ÿæˆã€æ‹¾å–ã€ä½¿ç”¨ï¼ˆå®‰æ”¾ç‚¸å¼¹ï¼‰åŠç›¸å…³éŸ³ç‰¹æ•ˆ)
    }

    private void Start()
    {
        m_LayerMask = LayerMask.GetMask("Obstacle");//±íÊ¾Ö±½Ó»ñµÃObstacleÕâ¸öLayer¶ÔÓ¦µÄLayerMask
        Destroy(gameObject, 8);
    }

    private void FixedUpdate()
    {
        // ×ªÉí
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, m_LayerMask);//´Ëº¯ÊıÓÃÓÚ¼ì²â2D³¡¾°ÖĞÄ³¸öÎ»ÖÃ£¨Vector2 point£©´¦ËùÓĞµÄÅö×²Ìå
        if (frontHits.Length > 0)
        {
            Flip();
        }
        // foreach (Collider2D c in frontHits)
        // {
        //   if (c.tag == "Obstacle")
        //  {
        //      Flip();
        //       break;// ×ªÉí & Í£Ö¹¼ì²âÆäËûÅö×²Ìå
        //   }
        // }

        // ÉèÖÃx·½ÏòËÙ¶È
        m_Rigidbody.velocity = new Vector2(transform.localScale.x * moveSpeed, m_Rigidbody.velocity.y);

        // Èç¹ûµĞÈË»¹ÓĞÒ»¸öÉúÃüÖµ¶øÇÒÓĞÒ»¸öÊÜÉËµÄsprite
        if (HP == 1 && damagedEnemy != null)
            ren.sprite = damagedEnemy;

        // Ã»ÑªÇÒdeath×´Ì¬Î´ĞŞ¸ÄÎªtrue
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
        // ÕÒ³öËùÓĞµÄ×é³É²¿·Ö
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

        // ÔÊĞíµĞÈËĞı×ª²¢Ìí¼ÓÒ»¸öÅ¤¾Ø
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));
=======
        dead = true;
        GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));// ÔÊĞíĞı×ª²¢Ìí¼ÓÒ»¸öÅ¤¾Ø
>>>>>>> parent of bc44874 (é“å…·ï¼ˆå¼¹è¯ç®±ã€åŒ»ç–—åŒ…ï¼‰ç”Ÿæˆã€æ‹¾å–ã€ä½¿ç”¨ï¼ˆå®‰æ”¾ç‚¸å¼¹ï¼‰åŠç›¸å…³éŸ³ç‰¹æ•ˆ)

        // Find all of the colliders on the gameobject and set them all to be triggers.
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        //Ëæ»ú²¥·ÅËÀÍöÒôĞ§
        int i = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

        // µÃ·Ö Create a vector that is just above the enemy.
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
