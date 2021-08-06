using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8.0f;
    //public float gravity = -20.0f;
    //float yVelocity = 0;

    CharacterController cc;

    public float hp;

    public float maxHp = 10.0f;

    public Slider hpSlider;

    public GameObject hitEffect;

    public int maxUp = 100;
    int upCount = 0;

    public float timeSpeed = 0.2f;
    //public float hpNow;


    Animator anim;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        hp = maxHp;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        hp -= timeSpeed * Time.deltaTime;
        hpSlider.value = hp / maxHp;

        if (hp <= 0.0f)
        {
            hp = 0.0f;
        }

        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (Input.GetKey(KeyCode.W))
        {

            cc.Move(transform.forward * moveSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.S))
        {

            cc.Move(transform.forward * -1f * moveSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.A))
        {

            cc.Move(transform.right * -1f * moveSpeed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.D))
        {

            cc.Move(transform.right * moveSpeed * Time.deltaTime);

        }



        // 땅 속으로 들어가거나 하늘을 날아가는 오류 수정 (방법2)
        // but 벽 통과 문제점 발생!!
        // 일단 방법2로 사용!!


        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);





        // enter키,CapsLock키 위로이동, Leftshift키 아래로 이동 구현
        // 0이 되면 더 아래로 내려가지 못하게
        // enter키,CapsLock키 위로이동
        if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.CapsLock)) && upCount < 500)
        {
            upCount++;
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        // shift키 아래로 이동
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && upCount > 0)
        {
            upCount--;
            transform.Translate((-1) * Vector3.up * moveSpeed * Time.deltaTime);
        }

        /*
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * moveSpeed * Time.deltaTime);
        */
        // 땅 속으로 들어가거나 하늘을 날아가는 오류 수정 (방법1)
        // but 중력으로인한 위,아래 이동이 불가능
    }

    public void OnDamage(int value)
    {
        hp -= (float)value;
        if (hp < 0.0f)
        {
            hp = 0.0f;
        }

        else
        {
            StartCoroutine(HitEffect());
        }
    }


    public void GetHp(int value) // 산소 획득으로 hp 증가
    {
        hp += (float)value;
        if (hp > 10.0f)
        {
            hp = 10.0f;
        }
    }


    IEnumerator HitEffect()
    {
        hitEffect.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        hitEffect.SetActive(false);
    }
}
