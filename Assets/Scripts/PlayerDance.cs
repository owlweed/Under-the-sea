using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDance : MonoBehaviour
{
    float hp;
    Animator anim;
    enum PlayerState
    {

        Dance
    }
    PlayerState playerState;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        playerState = PlayerState.Dance;
    }
}
