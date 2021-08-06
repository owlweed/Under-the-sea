using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
   enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
        //UpToChase

    }

    EnemyState enemyState;

    GameObject player;

    public float findDistance = 8.0f;

    CharacterController cc;

    public float moveSpeed=5.0f;

    public float attackDistance = 2.0f;

    float currentTime = 0;

    public float attackDelayTime = 2.0f;

    public int attackPower = 2;

    Vector3 originPos;
    Quaternion originRot;

    public float moveDistance = 20.0f;

    public int maxHp=5;

    int currentHp;

    public Slider hpSlider;

    Animator anim;

    //int enemyUpCount = 0;
    public int damagePower = 1; // 위로 올라갔을 때 닳는 hp

    NavMeshAgent smith;
    void Start()
    {
        enemyState = EnemyState.Idle;

        player = GameObject.Find("Player");

        cc = GetComponent<CharacterController>();

        originPos = transform.position;
        originRot = transform.rotation;

        currentHp = maxHp;

        anim = GetComponentInChildren<Animator>();

        smith = GetComponent<NavMeshAgent>();
        smith.speed = moveSpeed;
        smith.stoppingDistance = attackDistance;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:

                Idle();
                break;
              
            case EnemyState.Move:

                Move();
                break;

            case EnemyState.Attack:

                Attack();
                break;

            case EnemyState.Return:

                Return();
                break;

            case EnemyState.Damaged:

                //Damaged();
                break;

            case EnemyState.Die:

               //Die();
                break;

            /*
            case EnemyState.UpToChase:

                //Die();
                break;*/
        }
        
    }

    void Idle()
    {
        if(Vector3.Distance(player.transform.position, transform.position)<=findDistance)
        {
            enemyState = EnemyState.Move;
            print("상태 전환 : Idle->Move");
        }
        
    }

    void Move()
    {
        if(Vector3.Distance(originPos, transform.position) > moveDistance)
        {
            anim.SetTrigger("IdleToMove"); 
            enemyState = EnemyState.Return;
            print("상태 전환 : Move -> Return");
            
        }

       else if(Vector3.Distance(player.transform.position, transform.position)>attackDistance)
        {
            //Vector3 dir = (player.transform.position - transform.position).normalized;
            //transform.forward = dir;
            //cc.Move(dir * moveSpeed * Time.deltaTime);

            smith.SetDestination(player.transform.position);
            smith.stoppingDistance = attackDistance;

            anim.SetTrigger("IdleToMove");

        }

       else
        {
            enemyState = EnemyState.Attack;
            print("상태 전환 : Move - > Attack");
            anim.SetTrigger("MoveToAttackDelay");
            currentTime = attackDelayTime;

            if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.CapsLock)))
            {
                print("Hp down faster");
                PlayerMove pm = player.GetComponent<PlayerMove>();
                pm.OnDamage(damagePower);

            }

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                print("Hp down faster stop");
            }

            smith.isStopped = true;
            smith.ResetPath();
        }


    }

    void Attack()
    {

        if(Vector3.Distance(player.transform.position, transform.position)<=attackDistance)
        {
            if (currentTime >= attackDelayTime)
            {
                currentTime = 0;
                print("공격!");
                
                anim.SetTrigger("StartAttack");

                if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.CapsLock)))
                {
                    print("Hp down faster");
                    PlayerMove pm = player.GetComponent<PlayerMove>();
                    pm.OnDamage(damagePower);

                }

                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    print("Hp down faster stop");
                }
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        else
        {
            enemyState = EnemyState.Move;
            print("상태 전환 : Attack - > Move");

            anim.SetTrigger("AttackToMove");
        }
        
    }

    public void HitEvent()
    {
        PlayerMove pm = player.GetComponent<PlayerMove>();
        pm.OnDamage(attackPower);
    }

    void Return()
    {
        if(Vector3.Distance(originPos, transform.position)>0.5f)
        {
            //Vector3 dir = (originPos - transform.position).normalized;
            //transform.forward = dir;
            //cc.Move(dir * moveSpeed * Time.deltaTime);

            smith.SetDestination(originPos);
            smith.stoppingDistance = 0;
        }
        else
        {
            smith.isStopped = true;
            smith.ResetPath();

            transform.position = originPos;
            transform.rotation = originRot;

            enemyState = EnemyState.Idle;
            print("상태 전환 : Return - > Idle");
            anim.ResetTrigger("IdleToMove");
            anim.SetTrigger("MoveToIdle");

            currentHp = maxHp;
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(1.0f);

        enemyState = EnemyState.Move;
        print("상태 전환 : Damaged -> Move");
    }

    void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    public void HitEnemy(int value)
    {
        if(enemyState==EnemyState.Damaged || enemyState == EnemyState.Return || enemyState== EnemyState.Die)
        {
            return;
        }

        currentHp -= value;

        if(currentHp>0)
        {
            enemyState = EnemyState.Damaged;
            print("상태 전환 : Any state -> Damaged");
            anim.SetTrigger("Damaged");
            Damaged();
        }
        else
        {
            enemyState = EnemyState.Die;
            print("상태 전환 :Any state -> Die");
            anim.SetTrigger("Die");
            Die();
        }
    }

    /*
    void UpToChase()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
        {
            if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.CapsLock)))
            {
                print("상태 전환 :Chase");
                enemyUpCount++;
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }

            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                print("상태 전환 :Chase");
                enemyUpCount--;
                transform.Translate((-1) * Vector3.up * moveSpeed * Time.deltaTime);
            }
        }
   
    }*/
}
