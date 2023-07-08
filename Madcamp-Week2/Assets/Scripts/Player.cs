using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    public Animator anim;
    
    public bool isDead = false, facingRight = false;
    public int playerIdx = 0, playerLoc = 0;
    
    void Awake() {
        anim = gameObject.GetComponent<Animator>();
    }

}
