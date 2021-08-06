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



        // �� ������ ���ų� �ϴ��� ���ư��� ���� ���� (���2)
        // but �� ��� ������ �߻�!!
        // �ϴ� ���2�� ���!!


        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);





        // enterŰ,CapsLockŰ �����̵�, LeftshiftŰ �Ʒ��� �̵� ����
        // 0�� �Ǹ� �� �Ʒ��� �������� ���ϰ�
        // enterŰ,CapsLockŰ �����̵�
        if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.CapsLock)) && upCount < 500)
        {
            upCount++;
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        // shiftŰ �Ʒ��� �̵�
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
        // �� ������ ���ų� �ϴ��� ���ư��� ���� ���� (���1)
        // but �߷��������� ��,�Ʒ� �̵��� �Ұ���
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


    public void GetHp(int value) // ��� ȹ������ hp ����
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
