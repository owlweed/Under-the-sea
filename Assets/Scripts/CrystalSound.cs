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