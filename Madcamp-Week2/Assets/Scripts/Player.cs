using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Stairs stairs;
    public int playerLoc, score;
    public bool isDead;
    public bool facingRight;
    
    void Awake() {
        anim = gameObject.GetComponent<Animator>();
        stairs = GameObject.FindObjectOfType<Stairs>();
    }

    void Start() {
        playerLoc = 0;
        score = 0;
        isDead = false;
        facingRight = false;
    }

    public void Step(bool isStepRight) {

        // anim.SetBool("facingRight", isStepRight); // change sprite accordingly

        // check if correct
        if (stairs.isRight[playerLoc] == isStepRight) {
            // correct
            Debug.Log("Correct");
            score++;

            stairs.StairMove(isStepRight);
            playerLoc = (playerLoc + 1) % 20;
        } else {
            // wrong
            Debug.Log("Wrong");
            isDead = true;
        } 

        // reset timer
    }

}
