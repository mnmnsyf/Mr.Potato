using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShadow : MonoBehaviour
{
    public GameObject guiCopy;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 behindPos = transform.position;
        behindPos = new Vector3(guiCopy.transform.position.x, guiCopy.transform.position.y - 0.005f, guiCopy.transform.position.z - 1);
        transform.position = behindPos;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = guiCopy.GetComponent<Text>().text;
    }
}
