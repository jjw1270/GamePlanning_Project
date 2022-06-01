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
    AudioSource audioSource;
    public AudioClip walkSound, runSound;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        audioSource = this.GetComponent<AudioSource>();
    }
    void Update() {
        if(GameManager.isPaused) return;

        yVelocity += gravity * Time.deltaTime;
        
        if(speed == 5f){
            audioSource.clip = walkSound;
        }
        else{
            audioSource.clip = runSound;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
            speed = 8f;
        else if(Input.GetKeyUp(KeyCode.LeftShift))
            speed = 5f;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0 || v != 0){
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        else{
            if(audioSource.isPlaying)
                audioSource.Stop();
        }

        Vector3 dir = Vector3.right * h + Vector3.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}