using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] stairs;
    public GameObject LeftButton, RightButton;
    int score;

    void Awake() {
        // StairsInit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GameOver() {
        // gameover animation

        // ui
        ScoreBoard();

        // save score

        // stop timer
    }

    void ScoreBoard() {
        // show score
    }
}
