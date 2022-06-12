using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    public AudioClip pickupClip;        // 炸弹箱被捡起时的声音


    private Animator anim;              // 动画组件的引用
    private bool landed = false;		// 木箱是否已经落地

    void Start()
    {
        anim = transform.root.GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		// 如果玩家进入触发区域
		if (other.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			other.GetComponent<LayBombs>().bombCount++;
			Destroy(transform.root.gameObject);
		}
		else if (other.tag == "ground" && !landed)
		{
			anim.SetTrigger("Land");
			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;
		}
	}
}
