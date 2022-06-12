using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthBonus;               //  crate提供给玩家多少生命值
    public AudioClip collect;               // 收集箱子的声音


    private PickupSpawner pickupSpawner;    // 引用 pickup spawner.
    private Animator anim;                  // 引用 animator component.
    private bool landed;					// 箱子是否落地

    void Awake()
    {
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
        anim = transform.root.GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		// 如果玩家进入触发区域
		if (other.tag == "Player")
		{
			// 获取player health 的引用
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

			//通过生命值加成增加玩家的生命值，但将其限制在100
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

			// 更新血条
			playerHealth.UpdateHealthBar();

			// 触发新的投递
			pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

			// 播放收集声音
			AudioSource.PlayClipAtPoint(collect, transform.position);

			//销毁箱子
			Destroy(transform.root.gameObject);
		}
		//如果箱子撞到地面
		else if (other.tag == "ground" && !landed)
		{
			anim.SetTrigger("Land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;
		}
	}
}
