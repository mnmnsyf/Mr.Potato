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
        HeroBody = GetComponent<Rigidbody2D>();//��������ֵ����
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
        float h = Input.GetAxis("Horizontal");//�õ��������ҿ���
        //�����ƶ�
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);//Vector.right�ǣ�1��0��������������������
        }

        //��������ٶ�
        if (Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed, HeroBody.velocity.y);//Mathf.Sign���ţ��� f >= 0����1��Ϊ������-1��
        }

        //ת��
        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }

        //��Ծ
        if (Physics2D.Linecast(transform.position, mGroundCheck.position, 1 << LayerMask.NameToLayer("Ground")))//�����Ʊ�ʾ����
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;//�������
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
