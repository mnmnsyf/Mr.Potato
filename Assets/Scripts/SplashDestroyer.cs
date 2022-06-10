using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDestroyer : MonoBehaviour
{
	public bool destroyOnAwake;         // �Ƿ�Ӧ�����ӳٺ����� on Awake.
	public float awakeDestroyDelay;     // ��Awakeʱ���������ӳ�
	public bool findChild = false;      //�ҵ�һ������Ϸ����ɾ����
	public string namedChild;           //��Inspector�и��Ӷ�������
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
		// �����������Ϸ���� ���ԴӶ����¼��е���
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
