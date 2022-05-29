using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : MonoBehaviour
{
    public Transform target;
    public AudioClip getItemSound;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if(target != null){
            target.parent.GetComponent<AudioSource>().PlayOneShot(getItemSound);
            PlayerShoot.bulletCount+=10;
            target = null;
            Destroy(this.gameObject);
        }
    }

    private  void UpdateTarget(){
        Collider[] cols = Physics.OverlapSphere(transform.position, 2f, 1<<7);
        if(cols.Length > 0){
            Debug.Log("æ∆¿Ã≈€ »πµÊ");
            target = Camera.main.gameObject.transform;
        }
    }
}
