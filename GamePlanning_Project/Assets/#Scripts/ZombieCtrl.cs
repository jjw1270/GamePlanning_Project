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
    bool isAttack;
    Vector3 diePos;
    bool isItemDrop;
    AudioSource audioSource;
    private void Awake() {
        anim = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
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
        if(GameManager.isPaused) return;
        if(target != null){
            if(!audioSource.isPlaying)
                audioSource.Play();
            if(!isCollision){
                anim.SetBool("isRun", true);
                transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        //Debug.Log("공격");
        if(other.transform.CompareTag("Player")){
            anim.SetBool("isAttack", true);
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
            if(!isItemDrop){
                GameManager.deadZombieCount++;
                GameManager.zombieCount--;
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
            PlayerShoot.curHp -= 5;
            isAttack = true;
            Debug.Log("공격");
        }
        yield return new WaitForSeconds(3f);
        isAttack = false;
        
    }
}
