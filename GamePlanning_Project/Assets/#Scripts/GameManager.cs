using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int maxZombieCount = 50;
    public static int zombieCount = 0;
    public GameObject zombie;
    public static int deadZombieCount;
    float range_x, range_z;
    public static bool isPaused;
    public GameObject pauseCanvas;
    public GameObject smg;
    public GameObject ak;
    public Text quest;
    public GameObject bossZombie;
    public Text zomDieCount;
    void Start()
    {
        
    }

    void Update()
    {
        zomDieCount.text = deadZombieCount.ToString();
        
        if(deadZombieCount>=30){
            quest.text = "보스좀비를 처치하라!";
            //보스좀비
            Instantiate(bossZombie, new Vector3(100, 26, 100), Quaternion.Euler(new Vector3(0, Random.Range(0,360), 0)));
        }
        if(zombieCount<maxZombieCount){
            range_x = Random.Range(33, 164);
            range_z = Random.Range(40, 146);
            Vector3 randomPosition = new Vector3(range_x, 27, range_z);

            Instantiate(zombie, randomPosition, Quaternion.Euler(new Vector3(0, Random.Range(0,360), 0)));
            zombieCount++;
        }        
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(!isPaused){
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isPaused = true;
                Time.timeScale = 0;
                pauseCanvas.SetActive(true);
            }
            else{
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                isPaused = false;
                Time.timeScale = 1;
                pauseCanvas.SetActive(false);
            }
        }
    }

    public void smgBtn(){
        if(PlayerShoot.bulletCount>70){
            PlayerShoot.bulletCount -= 70;
            PlayerShoot.weaponNum = 2;
            if(GameObject.Find("PISTOL")){
                GameObject.Find("PISTOL").SetActive(false);
            }
            else if(GameObject.Find("AK")){
                GameObject.Find("AK").SetActive(false);
            }
            smg.SetActive(true);
        }
    }

    public void akBtn(){
        if(PlayerShoot.bulletCount>150){
            PlayerShoot.bulletCount -= 150;
            PlayerShoot.weaponNum = 3;
            if(GameObject.Find("PISTOL")){
                GameObject.Find("PISTOL").SetActive(false);
            }
            else if(GameObject.Find("SMG")){
                GameObject.Find("SMG").SetActive(false);
            }
            ak.SetActive(true);
        }
    }

    public void heal(){
        if(PlayerShoot.bulletCount>30){
            PlayerShoot.bulletCount -= 30;
            PlayerShoot.curHp += 30;
        }
    }
}