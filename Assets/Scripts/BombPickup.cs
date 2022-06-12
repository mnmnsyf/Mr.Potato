using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPickup : MonoBehaviour
{
    public AudioClip pickupClip;        // ը���䱻����ʱ������


    private Animator anim;              // �������������
    private bool landed = false;		// ľ���Ƿ��Ѿ����

    void Start()
    {
        anim = transform.root.GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		// �����ҽ��봥������
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
