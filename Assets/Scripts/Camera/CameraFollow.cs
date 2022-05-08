using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public float XSmooth = 8;//每秒移动的距离
    public float YSmooth = 8;
    public float XDistance = 2;//判断大于该距离则移动
    public float YDistance = 2; 
    public Vector2 MaxXandY;
    public Vector2 MinXandY;

    private Transform Hero;
    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool MoveX()
    {
        if (Mathf.Abs(Hero.position.x - transform.position.x) > XDistance)
            return true;
        else
            return false;
    }

    void FollowHero()
    {
        float newX = transform.position.x;
        float newY = transform.position.y;
        if (MoveX())
            newX = Mathf.Lerp(transform.position.x, Hero.position.x, XSmooth * Time.deltaTime);//从a移动到b
        newX = Mathf.Clamp(newX, MinXandY.x, MaxXandY.x);//存放到最小最大值之间

        transform.position = new Vector3(newX, newY,transform.position.z);
    }
    // Update is called once per frame
    void Update()//每一帧刷新
    {
        FollowHero();
    }
}
