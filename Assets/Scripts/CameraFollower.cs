using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
    Transform player;
    public float offset;
    private void Awake()
    {
        player = Transform.FindObjectOfType<Player>().transform;
    }
    private void Update()
    {
        if(!player)
            player = Transform.FindObjectOfType<Player>().transform;
        transform.position = new Vector3(0.0f, transform.position.y, player.position.z - offset);
    }
}
