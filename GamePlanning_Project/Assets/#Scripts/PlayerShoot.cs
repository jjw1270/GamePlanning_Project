using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Vector3 screenCenter;
    public bool isFire;
    float fireDelayTime;
    public static int weaponNum;
    private int weaponTmp;
    void Start()
    {
        isFire = false;
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        weaponTmp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(weaponNum != weaponTmp){
            weaponTmp = weaponNum;
        }

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 2000f)){
            if(Input.GetMouseButton(0) && !isFire){
                //shoot
                isFire = true;
                StartCoroutine(fireDelay());
                if(hit.transform.CompareTag("Enemy")){
                    
                }
            }
        }
    }
    IEnumerator fireDelay(){
        
        yield return new WaitForSeconds(fireDelayTime);   // 총기마다 연사시간 다름
        isFire = false;
    }
}
