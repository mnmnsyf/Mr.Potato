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
			//停止摄像机追踪玩家
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			//  停止生命条跟随玩家
			if (GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			//实例化玩家掉落的水花
			Instantiate(splash, col.transform.position, transform.rotation);
	
			Destroy(col.gameObject);
			// ...重新加载水平
			//StartCoroutine("ReloadGame");
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			//实例化敌人掉落的水花
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
