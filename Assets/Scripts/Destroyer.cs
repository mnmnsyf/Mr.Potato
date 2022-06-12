using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    protected AudioSource boomAudio;
    void Start()
    {
        boomAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyGameObject()//public：因为帧动画要调用这个函数
    {
        boomAudio.Play();
        Destroy(gameObject);//销毁自己
    }
}
