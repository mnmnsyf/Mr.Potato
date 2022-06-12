using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickups;                // Array of pickup prefabs with the bomb pickup first and health second.
    public float pickupDeliveryTime = 5f;       //�ӳ�Ͷ��
    public float dropRangeLeft;                 // ������������x����Сֵ���Է���Ͷ��
    public float dropRangeRight;                // ���ֵ
    public float highHealthThreshold = 75f;     // ��ҵ�����ֵ���ڴ�ֵ֮��ֻ��Ͷ��bomb crates
    public float lowHealthThreshold = 25f;		//��ҵ�����ֵ�����ڴ�ֵ��ֻ��health crates 


    private PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Start()
    {
        StartCoroutine(DeliverPickup());
    }

	// Update is called once per frame
	public IEnumerator DeliverPickup()
	{
		// �ȴ�Ͷ���ӳ�
		yield return new WaitForSeconds(pickupDeliveryTime);

		// ��drop��Χ�ڴ���һ�������x����
		float dropPosX = Random.Range(dropRangeLeft, dropRangeRight);

		// ����һ�����x�����λ��
		Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);

		if (playerHealth.health >= highHealthThreshold)			// �����ҵ�����ֵ���ڸ���ֵ
			Instantiate(pickups[0], dropPos, Quaternion.identity);
		else if (playerHealth.health <= lowHealthThreshold)		//���ڵ���ֵ
			Instantiate(pickups[1], dropPos, Quaternion.identity);
		else
		{
			int pickupIndex = Random.Range(0, pickups.Length);
			Instantiate(pickups[pickupIndex], dropPos, Quaternion.identity);
		}
	}
}
