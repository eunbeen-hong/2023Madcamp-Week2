using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public GameObject player, leftbutton, rightbutton;
    public GameObject[] stairs;

    // right: true / left: false
    public bool[] isRight = new bool[20];

    Vector3 startPos = new Vector3(0, -4.5f, 0);
    // float width = 1.0f;
    // float height = 0.5f;
    Vector3 dleft = new Vector3(-1f, 0.5f, 0);
    Vector3 dright = new Vector3(1f, 0.5f, 0);
    

    // Vector3 widthoffset = new Vector3(1.0f, 0, 0);

    Vector3 beforePos;

    enum State {start, leftDir, rightDir};
    State state = State.start;

    // Start is called before the first frame update
    void Awake() {
        StairInit();
    }

    void StairInit() {
        for (int i = 0; i < 20; i++) {
            switch(state) {
                case State.start:
                    stairs[i].transform.position = startPos;
                    state = State.leftDir;
                    isRight[i] = false;
                    break;
                case State.leftDir:
                    stairs[i].transform.position = beforePos + dleft;
                    break;
                case State.rightDir:
                    stairs[i].transform.position = beforePos + dright;
                    break;

            }
            beforePos = stairs[i].transform.position;

            // 1/3 확률로 방향 전환
            if (Random.Range(1, 9) < 3 && 0 < i && i < 19) {
                if (state == State.leftDir) {
                    state = State.rightDir;
                    isRight[i] = true;
                }
                else if (state == State.rightDir) {
                    state = State.leftDir;
                    isRight[i] = false;
                }
            }
        }
    }

    // 해당 번호 stair를 삭제 -> 재생성
    void StairSpawn(int num) {
        // 생성할 방향을 일단 현재 상태로 하고
        isRight[num == 19 ? 0 : num + 1] = state2bool(state);
        beforePos = stairs[num == 0 ? 19 : num - 1].transform.position;

        switch(state) {
            case State.leftDir:
                stairs[num].transform.position = beforePos + dleft;
                break;
            case State.rightDir:
                stairs[num].transform.position = beforePos + dright;
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

    void StairMove(bool stepRight) { // 플레이어가 오른쪽으로 움직임 -> stepRight = true
        // if (player.isDead) return;

        // Move stairs to the right or left
        Vector3 offset = stepRight ? (-dright) : (-dleft);
        for (int i = 0; i < 20; i++) {
            stairs[i].transform.position += offset;
        }

        // Respawn stairs below a certain height
        for (int i = 0; i < 20; i++) {
            if (stairs[i].transform.position.y < -5) StairSpawn(i);
        }

    }


    bool state2bool(State st) {
        switch(st) {
            case State.start:
                // not happen
            case State.leftDir:
                return false;
            case State.rightDir:
                return true;
        }
        // not happen
        return false;
    }

    // State bool2state(bool b) {
    //     if (b) {
    //         return State.rightDir;
    //     } else {
    //         return State.leftDir;
    //     }
    // }

    // Update is called once per frame
    void Update() {
        
    }
}
