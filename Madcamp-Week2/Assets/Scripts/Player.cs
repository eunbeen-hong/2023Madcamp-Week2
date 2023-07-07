using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public int playerLoc, score;
    public bool isDead;
    public bool facingRight;
    
    void Awake() {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Step(bool isStepRight) {
        if (isStepRight) {
            // change sprite
            facingRight = true;
        } else {
            // change sprite
            facingRight = false;
        }

        anim.SetBool("facingRight", facingRight); // change sprite accordingly

        // check if correct

        // stairs.StairMove(playerLoc, isStepRight);
        playerLoc = (playerLoc + 1) % 20;

        // reset timer
    }

}
