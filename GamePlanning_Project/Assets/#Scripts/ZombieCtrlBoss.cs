using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCtrlBoss : MonoBehaviour
{
    public Transform target;
    float moveSpeed = 5f;
    Animation anim;
    private int curHp = 1000;
    bool isCollision = false;
    public GameObject item;
    bool isAttack;
    Vector3 diePos;
    bool isItemDrop;
    private void Awake() {
        anim = this.GetComponent<Animation>();
    }
    void Start()
    {
        target = Camera.main.gameObject.transform;
    }

    private void Update() {
        if(GameManager.isPaused) return;
        if(target != null){
            if(!isCollision){
                anim.Play("Run");
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        //Debug.Log("공격");
        if(other.transform.CompareTag("Player")){
            Debug.Log("보스공격");
            anim.Play("Attack2");
            StartCoroutine(Attack());
            isCollision = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        //isCollision = false;
        //anim.Play("Run");
    }
    public void EnemyHP(int damage){
        curHp -= damage;
        if(curHp<=0){
            anim.Play("Death");
            target = null;
            moveSpeed = 0;
            if(!isItemDrop){
                Invoke("ItemDrop", 1f);
                isItemDrop = true;
            }
            Destroy(gameObject, 2f);
        }
    }
    void ItemDrop(){
        Instantiate(item, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.identity);
    }
    IEnumerator Attack(){
        if(!isAttack){
            PlayerShoot.curHp -= 15;
            isAttack = true;
            Debug.Log("공격");
        }
        yield return new WaitForSeconds(3f);
        isAttack = false;
        isCollision = false;
    }
}
