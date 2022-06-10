using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPropSpawner : MonoBehaviour
{
    public Rigidbody2D backgroundProp;      // 要实例化的道具
    public float leftSpawnPosX;             // 如果它在左边实例化，位置的x坐标
    public float rightSpawnPosX;            
    public float minSpawnPosY;              // 最低y坐标可能的位置
    public float maxSpawnPosY;              // 
    public float minTimeBetweenSpawns;      // 刷出间隔的最短时间
    public float maxTimeBetweenSpawns;      // 刷出间隔的最长时间
    public float minSpeed;                  // 道具的最低速度
    public float maxSpeed;					// 道具可能的最高速度
    void Start()
    {
        Random.InitState(System.DateTime.Today.Millisecond);
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	IEnumerator Spawn()//接口
	{
		// 在道具实例化之前创建一个随机的等待时间
		float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

		//等待指定的时间
		yield return new WaitForSeconds(waitTime);

		//出生朝向&位置
		bool facingLeft = Random.Range(0, 2) == 0;
		float posX = facingLeft ? rightSpawnPosX : leftSpawnPosX;
		float posY = Random.Range(minSpawnPosY, maxSpawnPosY);
		Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);

		Rigidbody2D propInstance = Instantiate(backgroundProp, spawnPos, Quaternion.identity) as Rigidbody2D;

		if (!facingLeft)
		{
			Vector3 scale = propInstance.transform.localScale;
			scale.x *= -1;
			propInstance.transform.localScale = scale;
		}

		// 创建一个随机速度.
		float speed = Random.Range(minSpeed, maxSpeed);
		speed *= facingLeft ? -1f : 1f;//速度方向默认朝右
		propInstance.velocity = new Vector2(speed, 0);

		// /重新启动协程生成另一个道具
		StartCoroutine(Spawn());

		// While the prop exists...
		while (propInstance != null)
		{
			if (facingLeft)
			{
				if (propInstance.transform.position.x < leftSpawnPosX - 0.5f)
					Destroy(propInstance.gameObject);
			}
			else
			{
				if (propInstance.transform.position.x > rightSpawnPosX + 0.5f)
					Destroy(propInstance.gameObject);
			}
			yield return null;
		}
	}
}
