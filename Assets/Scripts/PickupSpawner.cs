using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject[] pickups;                // Array of pickup prefabs with the bomb pickup first and health second.
    public float pickupDeliveryTime = 5f;       //延迟投递
    public float dropRangeLeft;                 // 在世界坐标中x的最小值可以发生投递
    public float dropRangeRight;                // 最大值
    public float highHealthThreshold = 75f;     // 玩家的生命值，在此值之上只会投递bomb crates
    public float lowHealthThreshold = 25f;		//玩家的生命值，低于此值的只有health crates 


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
		// 等待投递延迟
		yield return new WaitForSeconds(pickupDeliveryTime);

		// 在drop范围内创建一个随机的x坐标
		float dropPosX = Random.Range(dropRangeLeft, dropRangeRight);

		// 创建一个随机x坐标的位置
		Vector3 dropPos = new Vector3(dropPosX, 15f, 1f);

		if (playerHealth.health >= highHealthThreshold)			// 如果玩家的生命值高于高阈值
			Instantiate(pickups[0], dropPos, Quaternion.identity);
		else if (playerHealth.health <= lowHealthThreshold)		//低于低阈值
			Instantiate(pickups[1], dropPos, Quaternion.identity);
		else
		{
			int pickupIndex = Random.Range(0, pickups.Length);
			Instantiate(pickups[pickupIndex], dropPos, Quaternion.identity);
		}
	}
}
