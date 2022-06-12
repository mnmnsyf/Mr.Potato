using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UIManeger : MonoBehaviour
{
    public AudioMixer mixer;

    private bool mbPause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GamePause()
    {
        if (!mbPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        mbPause = !mbPause;

        Debug.Log("button clicked..");
    }

    public void GameVolumeChange(float fVloume)
    {
        //mixer.SetFloat("", fVloume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
