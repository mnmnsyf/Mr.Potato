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
    public bool isShooting = false;
    private Transform mGroundCheck;
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

        //速度 If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);// ... add a force to the player.
        }
        // If the player's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(HeroBody.velocity.x) > MaxSpeed)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);// ... set the player's velocity to the maxSpeed in the x axis.
        }

        // The Speed animator parameter is set to the absolute value of the horizontal input.
        anim.SetFloat("speed", Mathf.Abs(h));//绝对值


        //朝向 If the input is moving the player right and the player is facing left...
        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }

        //射线检测是通过按位与的操作进行而不是通过“==”操作进行判断
        // The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        if (Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
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
            // Add a vertical force to the player.
            HeroBody.AddForce(Vector2.up * JumpForce);
            
            anim.SetTrigger("Jump");//播放动画 Set the Jump animator trigger parameter.

            // Play a random jump audio clip.
            //int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            bJump = false;
        }
        if (isShooting)
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
        bFaceRight = !bFaceRight;// Switch the way the player is labelled as facing.
    }
}
