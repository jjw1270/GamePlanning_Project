using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCtrl : MonoBehaviour
{
    public Transform target;
    float moveSpeed = 3f;
    Animator anim;
    private int curHp = 100;
    bool isCollision = false;
    public GameObject item;
    Vector3 diePos;
    private void Awake() {
        anim = this.GetComponent<Animator>();
    }
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 1f);
    }

    private  void UpdateTarget(){
        Collider[] cols = Physics.OverlapSphere(transform.position, 12f, 1<<7);

        if(cols.Length > 0){
            //Debug.Log("플레이어 발견");
            target = Camera.main.gameObject.transform;
        }
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 12f);
    }
    private void Update() {
        if(target != null){
            if(!isCollision){
                anim.SetBool("isRun", true);
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        //Debug.Log("공격");
        if(other.transform.CompareTag("Player")){
            anim.SetBool("isAttack", true);
            Debug.Log("공격");
            StartCoroutine(Attack());
            isCollision = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        isCollision = false;
        anim.SetBool("isAttack", false);
    }
    public void EnemyHP(int damage){
        curHp -= damage;
        if(curHp<=0){
            anim.SetBool("isDie", true);
            target = null;
            moveSpeed = 0;
            Invoke("ItemDrop", 1f);
            Destroy(gameObject, 2f);
        }
    }
    void ItemDrop(){
        Instantiate(item, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);
    }
    IEnumerator Attack(){
        target.GetComponent<PlayerShoot>().curHp -= 5;
        yield return new WaitForSeconds(3f);
    }
}
