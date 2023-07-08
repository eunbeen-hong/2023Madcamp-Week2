using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // 시작 화면 관련(화면 전환, 로그인 버튼 등)
    public void OnClickStartButton() {
        // TODO: 로그인 처리

        SceneManager.LoadScene("GameScene");
    }
    
}
