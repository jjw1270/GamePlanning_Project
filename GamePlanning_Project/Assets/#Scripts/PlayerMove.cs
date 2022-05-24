using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpPower = 3f;
    float yVelocity;
    CharacterController cc;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update() {
        yVelocity += gravity * Time.deltaTime;
        
        if(cc.isGrounded){
            if(Input.GetButtonDown("Jump"))
                yVelocity = jumpPower;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
            speed = 10f;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            speed = 5f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}