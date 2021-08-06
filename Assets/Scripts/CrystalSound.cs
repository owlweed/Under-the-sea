using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalSound : MonoBehaviour
{
    //crystal.GetComponent<PickUp>();

    GameObject crystal;



    AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        crystal = GameObject.Find("Crystal");

    }

    // Update is called once per frame
    void Update()
    {
        //pickup스크립트가 사라지면 사운드 출력
        //if (Destroy(crystal))
        //{
        //aSource.Play();
        //}
    }

    public void PlaySound()
    {
        aSource.Play();
    }
}