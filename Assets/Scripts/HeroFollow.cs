using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFollow : MonoBehaviour
{
    private Transform player;		// Reference to the player.
    public Vector3 offset = new Vector3(0, 0.6f, 0); // The offset at which the Health Bar follows the player.
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position to the player's position with the offset.
        transform.position = player.position + offset;
    }
}
