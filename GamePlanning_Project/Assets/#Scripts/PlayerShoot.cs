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
    public GameObject bloodEffect;
    private float maxHp = 100f;
    public static float curHp = 100f;
    public Image hpBar;
    void Start()
    {
        weaponNum = 1;
        bulletCount = 30;
        isFire = false;
        screenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        weaponTmp = 0;
        curHp = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isPaused) return;
        hpBar.fillAmount = curHp / maxHp;

        bulletCountText.text = bulletCount.ToString();
        if(weaponNum != weaponTmp){
            weaponTmp = weaponNum;
            if(GameObject.Find("PISTOL")){
                gun = GameObject.Find("PISTOL");
                fireDelayTime = gun.transform.GetChild(0).GetComponent<Pistol>().fireDelay;
                gunDamage = gun.transform.GetChild(0).GetComponent<Pistol>().damage;
            }
            else if(GameObject.Find("SMG")){
                gun = GameObject.Find("SMG");
                fireDelayTime = gun.transform.GetChild(0).GetComponent<SMG>().fireDelay;
                gunDamage = gun.transform.GetChild(0).GetComponent<SMG>().damage;
            }
            else if(GameObject.Find("AK")){
                gun = GameObject.Find("AK");
                fireDelayTime = gun.transform.GetChild(0).GetComponent<AK>().fireDelay;
                gunDamage = gun.transform.GetChild(0).GetComponent<AK>().damage;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 2000f, Color.red);
        
        if(Input.GetMouseButton(0) && !isFire && bulletCount>0){
            //shoot
            isFire = true;
            StartCoroutine(fireDelay());

            if(Physics.Raycast(ray, out hit, 1000f, 1<<6)){
                Debug.Log("hit");
                GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                blood.transform.SetParent(hit.transform);
                if(hit.transform.name == "boss"){
                    ZombieCtrlBoss zc = hit.transform.GetComponent<ZombieCtrlBoss>();
                    zc.EnemyHP(gunDamage);
                    if(zc.target == null)
                        zc.target = Camera.main.transform;
                }
                else{
                    ZombieCtrl zc = hit.transform.GetComponent<ZombieCtrl>();
                    zc.EnemyHP(gunDamage);
                    if(zc.target == null)
                        zc.target = Camera.main.transform;
                }
            }
        }
    }
    
    IEnumerator fireDelay(){
        bulletCount--;
        gun.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(fireDelayTime);   // 총기마다 연사시간 다름
        gun.transform.GetChild(0).gameObject.SetActive(false);
        isFire = false;
    }
}
