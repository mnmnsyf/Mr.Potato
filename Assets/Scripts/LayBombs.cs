using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LayBombs : MonoBehaviour
{
    public bool bombLaid = false;       // �Ƿ���ը��������
    public int bombCount = 0;           // ���ӵ�ж���ը��
    public AudioClip bombsAway;         // ��ҷ���ը��ʱ������
    public GameObject bomb;				// ը����Ԥ����

    private Text bombHUD;			// ��ʾ����Ƿ���ը��

    void Awake()
    {
        bombHUD = GameObject.Find("ui_bombHUD").GetComponent<Text>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !bombLaid && bombCount > 0)
        {
            // ����ը������
            bombCount--;

            // ��bombLaid����Ϊtrue
            bombLaid = true;

            ///���ŷ���ը��������
            AudioSource.PlayClipAtPoint(bombsAway, transform.position);

            // ʵ����ը��
            Instantiate(bomb, transform.position, transform.rotation);
        }

        // ��������ը����ը��������ʾӦ�ñ����ã�����Ӧ�ñ�����
        //bombHUD.enabled = bombCount > 0;
    }
}
