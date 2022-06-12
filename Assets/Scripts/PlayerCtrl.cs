using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;
    [HideInInspector]
    public bool bJump = false;
    public float JumpForce = 500;
    public AudioClip[] jumpClips;			// Array of clips for when the player jumps.

    public AudioClip[] taunts;              // Array of clips for when the player taunts.
    public float tauntProbability = 50f;    // Chance of a taunt happening.
    public float tauntDelay = 1f;			// Delay for when the taunt should happen.

    private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
    private Transform mGroundCheck;
    private bool isShooting = false;
    Animator anim;

    
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();//控制动画
    }

    // Update is called once per frame
    void Update()
    {
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");

        //速度
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);// ... add a force to the player.
        }
        if (Mathf.Abs(HeroBody.velocity.x) > MaxSpeed)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);// ... set the player's velocity to the maxSpeed in the x axis.
        }

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("speed", Mathf.Abs(h));//绝对值


        //朝向
        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }

        //射线检测是通过按位与的操作进行而不是通过“==”操作进行判断
        // 通过射线检测是否与地面接触实现跳跃功能
        if (Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("jump1111.");
                bJump = true;
                Debug.Log("jump2222..");
            }
        }

        Debug.DrawLine(transform.position, mGroundCheck.position, Color.red);

        if (Input.GetButtonDown("Fire1"))
        {
            isShooting = true;
        }
    }

    private void FixedUpdate()
    {
        if (bJump)
        {
            //添加一个向上的力
            HeroBody.AddForce(Vector2.up * JumpForce);
            
            anim.SetTrigger("jump");//播放动画

            // Play a random jump audio clip.
            int i = UnityEngine.Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            bJump = false;
        }
        if (isShooting)//控制射击动画
        {
            isShooting = false;
            anim.SetTrigger("shooting");
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }

    public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = UnityEngine.Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);

			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();

				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}


	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = UnityEngine.Random.Range(0, taunts.Length);

		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}
