using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LayBombs : MonoBehaviour
{
    public bool bombLaid = false;       // 是否有炸弹被放置
    public int bombCount = 0;           // 玩家拥有多少炸弹
    public AudioClip bombsAway;         // 玩家放置炸弹时的声音
    public GameObject bomb;				// 炸弹的预设体

    private Text bombHUD;			// 显示玩家是否有炸弹

    void Awake()
    {
        bombHUD = GameObject.Find("ui_bombHUD").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
        {
            // 减少炸弹数量
            bombCount--;

            // 将bombLaid设置为true
            bombLaid = true;

            ///播放放置炸弹的声音
            AudioSource.PlayClipAtPoint(bombsAway, transform.position);

            // 实例化炸弹
            Instantiate(bomb, transform.position, transform.rotation);
        }

        // 如果玩家有炸弹，炸弹标题显示应该被启用，否则应该被禁用
        //bombHUD.enabled = bombCount > 0;
    }
}
