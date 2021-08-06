using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    float hp;
    Animator anim;
    enum PlayerState
    {
        
        Die
    }

    PlayerState playerState;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        playerState = PlayerState.Die;

        //switch (playerState)
        //{

        //case PlayerState.Die:

        // Die();
        //break;

        //}


        //void Die( )
        //{

        //}
    }
}
