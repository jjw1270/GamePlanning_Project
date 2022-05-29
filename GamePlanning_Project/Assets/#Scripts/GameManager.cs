using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int maxZombieCount = 50;
    public static int zombieCount = 0;
    public GameObject zombie;
    float range_x, range_z;
    void Start()
    {
        
    }

    void Update()
    {
        if(zombieCount<maxZombieCount){
            range_x = Random.Range(33, 164);
            range_z = Random.Range(40, 146);
            Vector3 randomPosition = new Vector3(range_x, 27, range_z);

            Instantiate(zombie, randomPosition, Quaternion.Euler(new Vector3(0, Random.Range(0,360), 0)));
            zombieCount++;
        }        

    }
}