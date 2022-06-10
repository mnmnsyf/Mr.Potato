using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticleSortingLayer1 : MonoBehaviour
{
    public string sortingLayerName;
    void Start()
    {
        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
