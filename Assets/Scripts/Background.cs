using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    public float parallaxFactor = 0.1f;//ÏµÊý
    public float framesParllaxFactor = 3f;
    public float smoothX = 10;
    public Transform[] Backgrounds;

    private Transform cam;
    private Vector3 camPrePos;


    private void Awake()
    {
        cam = Camera.main.transform;
    }
    void Start()
    {
        camPrePos = transform.position;
    }

    void bkParalax()
    {
        float fparallax = (camPrePos.x - cam.position.x) * parallaxFactor;
        for(int i = 0; i < Backgrounds.Length; ++i)
        {
            float bkNewX = Backgrounds[i].position.x + fparallax * (1 + i * framesParllaxFactor);
            Vector3 bkNewPos = new (bkNewX, Backgrounds[i].position.y, Backgrounds[i].position.z);
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, bkNewPos, Time.deltaTime * smoothX);
        }
        camPrePos = cam.position;
    }
    // Update is called once per frame
    void Update()
    {
        bkParalax();
    }
}
