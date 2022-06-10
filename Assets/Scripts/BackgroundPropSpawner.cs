using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundPropSpawner : MonoBehaviour
{
    public Rigidbody2D backgroundProp;      // Ҫʵ�����ĵ���
    public float leftSpawnPosX;             // ����������ʵ������λ�õ�x����
    public float rightSpawnPosX;            
    public float minSpawnPosY;              // ���y������ܵ�λ��
    public float maxSpawnPosY;              // 
    public float minTimeBetweenSpawns;      // ˢ����������ʱ��
    public float maxTimeBetweenSpawns;      // ˢ��������ʱ��
    public float minSpeed;                  // ���ߵ�����ٶ�
    public float maxSpeed;					// ���߿��ܵ�����ٶ�
    void Start()
    {
        Random.InitState(System.DateTime.Today.Millisecond);
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	IEnumerator Spawn()//�ӿ�
	{
		// �ڵ���ʵ����֮ǰ����һ������ĵȴ�ʱ��
		float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);

		//�ȴ�ָ����ʱ��
		yield return new WaitForSeconds(waitTime);

		//��������&λ��
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

		// ����һ������ٶ�.
		float speed = Random.Range(minSpeed, maxSpeed);
		speed *= facingLeft ? -1f : 1f;//�ٶȷ���Ĭ�ϳ���
		propInstance.velocity = new Vector2(speed, 0);

		// /��������Э��������һ������
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
