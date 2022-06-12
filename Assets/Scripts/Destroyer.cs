using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	public bool destroyOnAwake;         // 是否应该在延迟后销毁 on Awake.
	public float awakeDestroyDelay;     // 在Awake时销毁它的延迟
	public bool findChild = false;      //找到一个子游戏对象并删除它
	public string namedChild;           //在Inspector中给子对象命名
	void Awake()
	{
		// If the gameobject should be destroyed on awake,
		if (destroyOnAwake)
		{
			if (findChild)
			{
				Destroy(transform.Find(namedChild).gameObject);
			}
			else
			{
				// ... destroy the gameobject after the delay.
				Destroy(gameObject, awakeDestroyDelay);
			}

		}

	}


	void DestroyChildGameObject()
	{
		// 销毁这个子游戏物体 可以从动画事件中调用
		if (transform.Find(namedChild).gameObject != null)
			Destroy(transform.Find(namedChild).gameObject);
	}

	void DisableChildGameObject()
	{
		if (transform.Find(namedChild).gameObject.activeSelf == true)
			transform.Find(namedChild).gameObject.SetActive(false);
	}

	void DestroyGameObject()
	{
		Destroy(gameObject);
	}
}
