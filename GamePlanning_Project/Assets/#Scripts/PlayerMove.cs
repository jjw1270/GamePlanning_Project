using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    public float currentSpeed;
    public Transform cameraMain;
    public float jumpForce = 500;
    public Vector3 cameraPosition;
    public Transform bulletSpawn; //from here we shoot a ray to check where we hit him;
    private void Awake() {
        rb = GetComponent<Rigidbody>();
        cameraMain = transform.Find("Main Camera").transform;
        bulletSpawn = cameraMain.Find ("BulletSpawn").transform;
    }
    void Start()
    {
        
    }

    void FixedUpdate(){
        
    }
	private Vector2 horizontalMovement;
	public int maxSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        
    }
}
