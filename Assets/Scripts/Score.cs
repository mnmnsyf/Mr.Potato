using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;

    private PlayerCtrl playerCtrl;          //����player���ƽű�
    private int previousScore = 0;          //��ʼ����

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
        //���÷����ı�
        GetComponent<Text>().text = "Score: " + score;

        //��������ı�
        if (score != previousScore)
            playerCtrl.StartCoroutine(playerCtrl.Taunt());//Эͬ����

        previousScore = score;
    }
}
