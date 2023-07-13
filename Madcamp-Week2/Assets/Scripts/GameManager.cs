using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    
    public GameObject[] players, stairs, UI;
    public GameObject LeftButton, RightButton;
    public GameObject PauseButton, ResumeButton, RestartButton, StartButton, RankButton;
    public GameObject background;
    public Screaming screaming;
    public GameObject playerParent;
    
    public Animator anim;
    public Image timer;
    public TMP_Text finalScoreText, bestScoreText, currentScoreText;
    public Player player;


    public bool timerOn = false, gamePaused = false;
    float timerSpeed = 0.005f;
    int score;
    public int playerIdx = 0;


    public bool[] isRight = new bool[20];
    Vector3 prevStairPos;
    Vector3 startPos = new Vector3(0, -4.5f, 0);
    Vector3 dleft = new Vector3(-1.2f, 0.8f, 0);
    Vector3 dright = new Vector3(1.2f, 0.8f, 0);

    enum State {start, leftDir, rightDir};
    State state = State.start;

    // Admin
    public bool buttonAdmin = false, timerAdmin = false;

    public UserScriptObject userObj;

    public UrlObject URL;

    void Awake() {
        // 여기서 userObj 처리? (DB에서 받아오기?)
    }

    void Start() {
        if (userObj.id == "0000") {
            Invoke("UserInit", 2);    
        } else {
            UserInit();
        }
        

        StairInit();
        Timer();
        StartCoroutine("CheckTimer");
    }

    void UserInit() {
        playerIdx = SchoolNameToIdx(userObj.univ);
        players[playerIdx].SetActive(true);
        player = players[playerIdx].GetComponent<Player>();
        playerParent = player.transform.parent.gameObject;
        playerParent.SetActive(true);
        StartButton.SetActive(true);
        RankButton.SetActive(true);
    }

    int SchoolNameToIdx(string SchoolName) {
        switch (SchoolName) {
            case "GIST": return 1;
            case "한양대": return 2;
            case "KAIST": return 3;
            case "고려대": return 4;
            case "성균관대": return 5;
            case "숙명여대": return 6;
            case "POSTECH": return 7;
            default: return 0;
        }
    }

    // 시작 화면 관련(화면 전환, 로그인 버튼 등)
    public void OnClickStartButton() {
        // TODO: 로그인 처리

        SceneManager.LoadScene("GameScene");
    }

    ////////////////////Stairs////////////////////
    void StairInit() {
        for (int i = 0; i < 20; i++) {
            switch(state) {
                case State.start:
                    stairs[i].transform.position = startPos;
                    state = State.leftDir;
                    isRight[i] = false;
                    break;
                case State.leftDir:
                    stairs[i].transform.position = prevStairPos + dleft;
                    break;
                case State.rightDir:
                    stairs[i].transform.position = prevStairPos + dright;
                    break;

            }
            prevStairPos = stairs[i].transform.position;

            // 1/3 확률로 방향 전환
            if (Random.Range(1, 9) < 3 && 0 < i && i < 19) { // 왜 0, 19번째는 방향 전환 안함?
                if (state == State.leftDir) {
                    state = State.rightDir;
                    isRight[i] = true;
                }
                else if (state == State.rightDir) {
                    state = State.leftDir;
                    isRight[i] = false;
                }
            } else {
                isRight[i] = state2bool(state);
            }
        }
    }

    // 해당 번호 stair를 삭제 -> 재생성
    void StairSpawn(int num) {
        // 생성할 방향을 일단 현재 상태로 하고
        isRight[num] = state2bool(state);
        prevStairPos = stairs[(num - 1 + 20) % 20].transform.position;

        switch(state) {
            case State.leftDir:
                stairs[num].transform.position = prevStairPos + dleft;
                break;
            case State.rightDir:
                stairs[num].transform.position = prevStairPos + dright;
                break;
        }
        
        // 1/3 확률로 방향 전환
        if (Random.Range(1, 9) < 3) {
            if (state == State.leftDir) {
                state = State.rightDir;
                isRight[num] = true;
            }
            else if (state == State.rightDir) {
                state = State.leftDir;
                isRight[num] = false;
            }
        }

    }    
    
    public void StairMove(bool isStepRight) { // 플레이어가 오른쪽으로 움직임 -> stepRight = true
        if (player.isDead) return;

        // Move stairs to the right or left
        Vector3 offset = isStepRight ? (-dright) : (-dleft);
        for (int i = 0; i < 20; i++) {
            stairs[i].transform.position += offset;
        }

        // Respawn stairs below a certain height
        for (int i = 0; i < 20; i++) {
            if (stairs[i].transform.position.y < -7) StairSpawn(i);
        }

        if (isRight[player.playerLoc] != isStepRight) {
            Debug.Log("Wrong");
            setControlBtn(false);
            GameOver();
            return;
        }
        
        Debug.Log("Correct");

        score++;
        currentScoreText.text = score.ToString();
        timer.fillAmount += 0.7f ;
        // ResetTimer();
        background.transform.position += background.transform.position.y < -14f ?
            new Vector3(0, 4.7f, 0) : new Vector3(0, -0.05f, 0);
    }
    
    bool state2bool(State st) {
        return st == State.rightDir;
    }

    void GameOver() {
        // gameover animation
        anim.SetBool("GameOver", true);
        player.anim.SetBool("Die", true);

        //screaming
        screaming.PlayAudio();

        // Change UI
        ScoreBoard();
        PauseButton.SetActive(false);
        setControlBtn(false);

        player.isDead = true;
        // player.MoveAnimation();
        // if (vibrationOn) Vibration();

        // CancelInvoke();
        Invoke("GameOverScene", 2f);
    }

    private void setControlBtn(bool isOn) {
        LeftButton.SetActive(isOn);
        RightButton.SetActive(isOn);
    }

    void ScoreBoard() {
        finalScoreText.text = score.ToString();

        // if highest score, record
        if (score > userObj.bestScore) {
            userObj.bestScore = score;
            bestScoreText.text = score.ToString();

            var url = string.Format("{0}/{1}", URL.host, URL.urlUpdateScore);
            Debug.Log(url);

            var req = new Protocols.Packets.req_UpdateUser();
            req.id = userObj.id;
            req.bestScore = score;
            var json = JsonConvert.SerializeObject(req);
            Debug.Log(json);

            StartCoroutine(RankMain.UpdateScore(url, json, (raw) => {
                // var res = JsonConvert.DeserializeObject<Protocols.Packets.res>(raw);
                Debug.LogFormat("UPDATED {0} -> {1}", req.id, req.bestScore);

            }));


        } else {
            bestScoreText.text = userObj.bestScore.ToString();
        }
    }

    void GameOverScene() {
        UI[0].SetActive(false);
    }

    ////////////////////Buttons////////////////////
    public void BtnClick(GameObject btn) {
        switch (btn.name) {
            case "LeftButton":
                Debug.Log("LeftButton clicked");
                break;
            case "RightButton":
                Debug.Log("RightButton clicked");
                break;
            case "RestartButton":
                Debug.Log("RestartButton clicked");
                LoadScene(0);
                UI[0].SetActive(true); // inGameUI
                UI[2].SetActive(false); // homeUI
                break;
            case "PauseButton":
                Debug.Log("PauseButton clicked");
                CancelInvoke();
                PauseButton.SetActive(false);
                ResumeButton.SetActive(true);
                gamePaused = true;
                break;
            case "ResumeButton":
                Debug.Log("ResumeButton clicked");
                PauseButton.SetActive(true);
                ResumeButton.SetActive(false);
                Timer();
                gamePaused = false;
                break;
            default:
                Debug.Log("Unknown Button "+btn.name);
                break;
        }
    }

    private void PlayerStepAnim(GameObject btn) {
        if (btn.name == "RightButton" && isFacingLeft(playerParent)) {
            playerParent.transform.rotation = Quaternion.Euler(0, 180, 0);
            Debug.Log(playerParent.transform.rotation);
        } else if (btn.name == "LeftButton" && !isFacingLeft(playerParent)) {
            playerParent.transform.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log(playerParent.transform.rotation);
        }
    }

    public void BtnDown(GameObject btn) {
        btn.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        
        switch (btn.name) {
            case "LeftButton":
                Debug.Log("LeftButton clicked");
                // StartCoroutine(PlayerStepAnim(btn));
                PlayerStepAnim(btn);
                Step(false);
                break;
            case "RightButton":
                Debug.Log("RightButton clicked");
                // StartCoroutine(PlayerStepAnim(btn));
                PlayerStepAnim(btn);
                Step(true);
                break;
        }
    }

    public void BtnUp(GameObject btn) {
        btn.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void LoadScene(int i) {
        SceneManager.LoadScene(i);
    }

    ////////////////////Players////////////////////
    public void Step(bool isStepRight) {
        // anim.SetBool("facingRight", isStepRight); // change sprite accordingly

        if (buttonAdmin) {
            isStepRight = isRight[player.playerLoc];
        }

        StairMove(isStepRight);
        player.playerLoc = (player.playerLoc + 1) % 20;

        timerOn = true;
    }

    ////////////////////Timer////////////////////
    private void Timer() {
        if (timerOn){
            if (score > 30) timerSpeed = 0.0055f;
            if (score > 60) timerSpeed = 0.006f;
            if (score > 100) timerSpeed = 0.0065f;
            if (score > 150) timerSpeed = 0.007f;
            if (score > 200) timerSpeed = 0.008f;
            if (score > 300) timerSpeed = 0.01f;
            if (score > 400) timerSpeed = 0.015f;
            if (score > 600) timerSpeed += 0.0001f;

            timer.fillAmount -= timerSpeed;
        }

        Invoke("Timer", 0.01f);
    }

    public void ResetTimer() {
        timer.fillAmount = 1;
    }

    IEnumerator CheckTimer() {
        while (timer.fillAmount > 0) {
            yield return new WaitForSeconds(0.4f);
        }
        if (timerAdmin == false)
            GameOver();
    }

    private bool isFacingLeft(GameObject playerParent) {
        return playerParent.transform.rotation == Quaternion.Euler(0, 0, 0);
    }
}
