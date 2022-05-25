using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    private Vector3 screenCenter;
    public bool isFire;
    float fireDelayTime = 0.5f;
    int gunDamage = 10;
    public static int weaponNum = 1;
    private int weaponTmp = 99;
    private GameObject gun;
    public static int bulletCount = 30;
    public Text bulletCountText;
    void Start()
    {
        isFire = false;
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        weaponTmp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bulletCountText.text = bulletCount.ToString();
        if(weaponNum != weaponTmp){
            weaponTmp = weaponNum;
            gun = GameObject.Find("Gun");
            switch(gun.transform.GetChild(0).name){
                case "PISTOL":
                    fireDelayTime = gun.transform.GetChild(0).GetComponent<Pistol>().fireDelay;
                    gunDamage = gun.transform.GetChild(0).GetComponent<Pistol>().damage;
                    break;
                case "SMG":
                    fireDelayTime = gun.transform.GetChild(0).GetComponent<SMG>().fireDelay;
                    gunDamage = gun.transform.GetChild(0).GetComponent<SMG>().damage;
                    break;
                case "AK":
                    fireDelayTime = gun.transform.GetChild(0).GetComponent<AK>().fireDelay;
                    gunDamage = gun.transform.GetChild(0).GetComponent<AK>().damage;
                    break;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2000f, Color.red);
        
        if(Input.GetMouseButton(0) && !isFire && bulletCount>0){
            //shoot
            isFire = true;
            StartCoroutine(fireDelay());

            if(Physics.Raycast(ray, out hit, 2000f, 1<<6)){
                //if(hit.transform.CompareTag("Enemy")){
                    Debug.Log("적 히또");
                //}
            }
        }
        
        // if(Physics.Raycast(ray, out hit, 2000f, 1<<6)){
        //     //Debug.Log("fff");
        //     if(Input.GetMouseButton(0) && !isFire){
        //         //shoot
        //         isFire = true;
        //         StartCoroutine(fireDelay());
        //         if(hit.transform.CompareTag("Enemy")){
        //             Debug.Log("적 히또");
        //         }
        //     }
        // }
    }
    IEnumerator fireDelay(){
        bulletCount--;
        gun.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(fireDelayTime);   // 총기마다 연사시간 다름
        gun.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        isFire = false;
    }
}
