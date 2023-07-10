using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScene : MonoBehaviour
{
    private AndroidJavaObject ajo;
    public void OnClickStartButton() {
        // 로그인 처리 -> 아마 사용자 이름 받아옴?
        // ajo = new AndroidJavaObject( "com.example.madcamp_week2.UKakao" );
        // ajo.Call( "KakaoLogin" );
        // 로그인 성공 시
        SceneManager.LoadScene("GameScene"); //GameScene으로 전환
    }


    
}
