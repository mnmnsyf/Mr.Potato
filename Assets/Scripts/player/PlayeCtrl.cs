using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector] public bool bFaceRight = true;
    [HideInInspector] public bool bJump = false;
    public float JumpForce = 100;
    private Transform mGroundCheck;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();//给变量赋值变量
        mGroundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {
        movement();

    }

    private void FixedUpdate()
    {
        if (bJump)
        {
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
        }
    }

    private void movement()
    {
        float h = Input.GetAxis("Horizontal");//得到键盘左右控制
        //控制移动
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);//Vector.right是（1，0）向量，决定方向向右
        }

        //限制最大速度
        if (Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);//Mathf.Sign符号，当 f >= 0返回1，为负返回-1。
        }

        //转身
        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }

        //跳跃
        if (Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))//二进制表示层数
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;//获得属性
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
