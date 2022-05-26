using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject explosion;//爆炸效果的预设体
    void Start()
    {
        Destroy(gameObject, 1);//1s后销毁自己
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰到的对象
    {
        if(collision.gameObject.tag != "Player")
        {
            float rotation = Random.Range(0, 360);//随机旋转量
            Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, rotation));//实例化爆炸效果，z方向实现旋转
            Destroy(gameObject);//销毁炮弹自己
        }
    }
}
