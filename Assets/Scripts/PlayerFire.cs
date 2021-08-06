using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    public Transform firePosition;


    public GameObject bulletEffect;
    ParticleSystem ps;

    AudioSource aSource;

    public int attackPower = 2;

    Animator anim;

  

    public GameObject[] eff_Flash;
    void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();

        aSource = GetComponent<AudioSource>();

        anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(attackPower);
                }

                Instantiate(bulletEffect, transform.position, transform.rotation);
                ps.Play();

            }
            aSource.Play();

            if (anim.GetFloat("MoveDirection") == 0)
            {
                anim.SetTrigger("Attack");
            }

            StartCoroutine(ShootEffect(0.1f));

        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Camera.main.fieldOfView = 60.0f;


            weapon01.SetActive(true);
            weapon02.SetActive(false);
            crosshair01.SetActive(true);
            crosshair02.SetActive(false);

            weapon01_R.SetActive(true);
            weapon02_R.SetActive(false);

            crosshair02_zoom.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapon01.SetActive(false);
            weapon02.SetActive(true);
            crosshair01.SetActive(false);
            crosshair02.SetActive(true);

            weapon01_R.SetActive(false);
            weapon02_R.SetActive(true);
        }
    }

    IEnumerator ShootEffect(float duration)
    {
        int num = Random.Range(0, eff_Flash.Length - 1);
        eff_Flash[num].SetActive(true);
        yield return new WaitForSeconds(duration);
        eff_Flash[num].SetActive(false);
    }
}
