using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCtrl : MonoBehaviour
{
    Transform target;
    float moveSpeed = 3f;
    Animator anim;
    private int maxHp = 100;
    private int curHp = 100;
    private void Awake() {
        anim = this.GetComponent<Animator>();
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    private  void UpdateTarget(){
        Collider[] cols = Physics.OverlapSphere(transform.position, 10f, 1<<7);

        if(cols.Length > 0){
            Debug.Log("플레이어 발견");
            target = cols[0].gameObject.transform;
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
    private void Update() {
        if(target != null){
            anim.SetBool("isRun", true);
            Vector3 dir = -target.position + transform.position;
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Player")){
            anim.SetBool("isAttack", true);
            Debug.Log("공격");
        }
    }
    public void EnemyHP(int damage){
        
    }
}
