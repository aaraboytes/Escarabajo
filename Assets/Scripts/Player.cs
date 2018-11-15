using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed,acc = 0.1f,jumpPower = 100,maxSpeed;
    public float maxX, minX;
    CharacterController player;
    Vector3 move;
    public Camera cam;
	// Use this for initialization
	void Start () {
        player = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //Cam axis//
        float angle = cam.transform.rotation.eulerAngles.z;
        if (angle > 0 && angle < 90)
        {
            angle *= -1;
        }else if(angle > 270)
        {
            angle = Mathf.Abs(angle - 365);
        }
        //Set axis -1 o 1
        float headAxis = ((2 * angle) / 180);
        Debug.Log(headAxis);
            
        if(speed<maxSpeed)
            speed += Time.deltaTime * acc;
        //move.x = Input.GetAxis("Horizontal") * speed;
        move.x = headAxis * speed;
        move.z = speed;
        if (!player.isGrounded)
            move.y = Physics.gravity.y;
        else
            move.y = 0;
        if (Input.GetButtonDown("Jump"))
            move.y = jumpPower;
        //Set limits
        if (transform.position.x <= minX)
            move.x = speed * 0.05f;
        else if (transform.position.x >= maxX)
            move.x = -speed *0.05f;
        player.Move(move * Time.deltaTime);
	}
}
