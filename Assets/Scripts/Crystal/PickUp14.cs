using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp14 : MonoBehaviour
{
    GameObject crystal;
    //GameObject oxygen; //**

    public float discoverDistance = 5.0f;
    public GameObject crystalSound;

    //public AudioClip audiodie;
    GameObject player;

    private Ray ray;
    private RaycastHit hit;
    //public int oxygenHp = 4; //**

    AudioSource aSource;



    // Start is called before the first frame update
    void Start()
    {
        crystal = GameObject.Find("Crystal14");
        player = GameObject.Find("Player");
        //oxygen = GameObject.Find("Oxygen"); //**
        aSource = GetComponent<AudioSource>();
    }




    // Update is called once per frame
    void Update()
    {
        //float distance1 = Vector3.Distance(player.transform.position, crystal.transform.position);
        float distance1 = Vector3.Distance(player.transform.position, gameObject.transform.position);
        //float distance2 = Vector3.Distance(player.transform.position, oxygen.transform.position);


        if (Input.GetMouseButtonDown(1))
        {
            //aSource.Play();
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {
                //aSource.Play();
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Crystal") && distance1 <= discoverDistance)
                {
                    crystalSound.GetComponent<CrystalSound>().PlaySound();

                    //aSource.Play();
                    print("destroy");

                    GameObject smObject = GameObject.Find("ScoreManager");
                    ScoreManager sm = smObject.GetComponent<ScoreManager>();
                    //aSource.Stop();
                    sm.Score += 10;

                    sm.ScoreUI.text = "Á¡¼ö:" + sm.Score;

                    Destroy(gameObject);
                }

            }


        }


    }
}