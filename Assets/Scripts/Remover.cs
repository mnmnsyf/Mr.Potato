using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Remover : MonoBehaviour
{
    public GameObject splash;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			//ֹͣ�����׷�����
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			//  ֹͣ�������������
			if (GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			//ʵ������ҵ����ˮ��
			Instantiate(splash, col.transform.position, transform.rotation);
	
			Destroy(col.gameObject);
			// ...���¼���ˮƽ
			//StartCoroutine("ReloadGame");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			//ʵ�������˵����ˮ��
			Instantiate(splash, col.transform.position, transform.rotation);

			Destroy(col.gameObject);
		}
	}

	IEnumerator ReloadGame()
	{
		// ... pause briefly
		yield return new WaitForSeconds(2);
		// ... and then reload the level.
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}
}
