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

    void OnExplode()
    {
        // 创建一个在z轴上随机旋转的四元数
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

        // 在随机旋转的火箭处实例化爆炸
        Instantiate(explosion, transform.position, randomRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)//碰到的对象
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hurt();
            OnExplode();
            Destroy(gameObject);
        }
        // Otherwise if it hits a bomb crate...
        //else if (collision.tag == "BombPickup")
        //{
        //    // ... find the Bomb script and call the Explode function.
        //    collision.gameObject.GetComponent<Bomb>().Explode();

        //    // Destroy the bomb crate.
        //    Destroy(collision.transform.root.gameObject);

        //    // Destroy the rocket.
        //    Destroy(gameObject);
        //}
        else if(collision.gameObject.tag != "Player")
        {
            float rotation = Random.Range(0, 360);//随机旋转量
            Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, rotation));//实例化爆炸效果，z方向实现旋转
            Destroy(gameObject);//销毁炮弹自己
        }
    }
}
