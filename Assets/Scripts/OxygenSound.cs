using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenSound : MonoBehaviour
{
    //crystal.GetComponent<PickUp>();

    GameObject oxygen;



    AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        oxygen = GameObject.Find("Oxygen");

    }

    // Update is called once per frame
    void Update()
    {
        //pickup��ũ��Ʈ�� ������� ���� ���
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