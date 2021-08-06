using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOxygen2 : MonoBehaviour
{
    GameObject player;
    GameObject oxygen;

    public float discoverDistance = 5.0f;

    public GameObject oxygenSound;

    private Ray ray;
    private RaycastHit hit;
    public int oxygenHp = 4;

    AudioSource aSource;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        oxygen = GameObject.Find("Oxygen2");
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance2 = Vector3.Distance(player.transform.position, oxygen.transform.position);

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray, out hitInfo))
            {

                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Oxygen") && distance2 <= discoverDistance)
                {

                    oxygenSound.GetComponent<OxygenSound>().PlaySound();
                    print("destroy");

                    PlayerMove pm = player.GetComponent<PlayerMove>();
                    pm.GetHp(oxygenHp);
                    Destroy(gameObject);
                }

            }


        }
    }
}