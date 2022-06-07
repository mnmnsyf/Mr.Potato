using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;

    private PlayerCtrl playerCtrl;          //引用player控制脚本
    private int previousScore = 0;          //开始分数

    private void Awake()
    {
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //设置分数文本
        GetComponent<Text>().text = "Score: " + score;

        //如果分数改变
        if (score != previousScore)
            playerCtrl.StartCoroutine(playerCtrl.Taunt());//协同程序

        previousScore = score;
    }
}
