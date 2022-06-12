using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthBonus;               //  crate�ṩ����Ҷ�������ֵ
    public AudioClip collect;               // �ռ����ӵ�����


    private PickupSpawner pickupSpawner;    // ���� pickup spawner.
    private Animator anim;                  // ���� animator component.
    private bool landed;					// �����Ƿ����

    void Awake()
    {
        pickupSpawner = GameObject.Find("pickupManager").GetComponent<PickupSpawner>();
        anim = transform.root.GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		// �����ҽ��봥������
		if (other.tag == "Player")
		{
			// ��ȡplayer health ������
			PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

			//ͨ������ֵ�ӳ�������ҵ�����ֵ��������������100
			playerHealth.health += healthBonus;
			playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);

			// ����Ѫ��
			playerHealth.UpdateHealthBar();

			// �����µ�Ͷ��
			pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());

			// �����ռ�����
			AudioSource.PlayClipAtPoint(collect, transform.position);

			//��������
			Destroy(transform.root.gameObject);
		}
		//�������ײ������
		else if (other.tag == "ground" && !landed)
		{
			anim.SetTrigger("Land");

			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;
		}
	}
}
